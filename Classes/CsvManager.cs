using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Reflection;

// alternatively use Nuget:using CsvHelper;
namespace CSharpAppPlayground.Classes
{
    public class CsvManager
    {
        // TODO: read and write to CSV files class
        private string filePath { set; get; }

        private char separator { set; get; } = ':';

        public CsvManager(string _filePath, char _separator = ':')
        {
            filePath = _filePath;
            separator = _separator;
        }

        public string GetFilePath() { return filePath; }

        private static bool IsSimpleType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = Nullable.GetUnderlyingType(type) ?? type;
            }

            if (type.IsEnum) return true;

            return type.IsPrimitive ||
                   type == typeof(string) ||
                   type == typeof(decimal) ||
                   type == typeof(long) ||
                   type == typeof(BigInteger) ||
                   type == typeof(DateTime) ||
                   type == typeof(DateTimeOffset) ||
                   type == typeof(TimeSpan) ||
                   type == typeof(Guid);
        }

        private static void PrintProperties(IOrderedEnumerable<PropertyInfo> props)
        {
            Debug.Print("Properties");
            foreach(PropertyInfo prop in props)
            {
                Debug.Print($"prop name: {prop.Name}");
            }
        }

        private static string EscapeValue(string? raw, char separator)
        {
            if (raw == null) return string.Empty;

            bool needsQuoting = raw.IndexOfAny(new[] { '\"', '\n', '\r', separator }) >= 0;
            string escaped = raw.Replace("\"", "\"\"");
            return needsQuoting ? "\"" + escaped + "\"" : escaped;
        }

        private static string FormatValue(object? value)
        {
            if (value == null) return string.Empty;

            switch (value)
            {
                case DateTime dt:
                    return dt.ToString("o", CultureInfo.InvariantCulture); // ISO 8601
                case DateTimeOffset dto:
                    return dto.ToString("o", CultureInfo.InvariantCulture);
                case IFormattable formattable:
                    return formattable.ToString(null, CultureInfo.InvariantCulture);
                default:
                    return value.ToString() ?? string.Empty;
            }
        }

        public bool WriteToCSV<T>(IEnumerable<T> objects, char separator = ':') 
        {
            bool success = true;
            try
            {
                Type itemType = typeof(T);
                IOrderedEnumerable<PropertyInfo> props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                    .Where(p => p.CanRead && IsSimpleType(p.PropertyType))
                                    .OrderBy(p => p.Name); // Get and sort properties

                PrintProperties(props);

                // 1. create the folders to the filePath
                Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(filePath)) ?? ".");

                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    string header = string.Join(separator, props.Select(p => EscapeValue(p.Name, separator)));
                    sw.WriteLine(header);

                    foreach (T obj in objects)
                    {
                        IEnumerable<string> values = props.Select(p => EscapeValue(FormatValue(p.GetValue(obj)), separator));
                        sw.WriteLine(string.Join(separator, values));
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.Print("Failed WriteToCSV()");
                success = false;
            }
            return success;
        }

        public static string[] ParseLineRobust(string line, char separator)
        {
            string _separator = separator.ToString();
            // TextFieldParser expects a stream or file path.
            // We use StringReader to simulate a file stream from our single line.
            using (var reader = new StringReader(line))
            using (var parser = new TextFieldParser(reader))
            {
                parser.SetDelimiters(_separator); // Set the colon as the delimiter
                parser.HasFieldsEnclosedInQuotes = true; // Respect quotes

                if (!parser.EndOfData)
                {
                    return parser.ReadFields();
                }
            }
            return Array.Empty<string>();
        }

        public List<T> ReadFromCSV<T>() where T : new(){

            var records = new List<T>();
            try
            {
                var lines = File.ReadAllLines(filePath);

                if (lines.Length == 0)
                    return records;

                // 1. Get Headers and Property Information
                // This simple split fails if a header name contains a comma
                var headers = lines[0].Split(separator);
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // Create a mapping from header index to the corresponding PropertyInfo object
                var headerMap = new Dictionary<int, PropertyInfo>();
                for (int i = 0; i < headers.Length; i++)
                {
                    // Find a property where the name matches the header name (case-insensitive search)
                    var prop = properties.FirstOrDefault(p => p.Name.Equals(headers[i].Trim(), StringComparison.OrdinalIgnoreCase));
                    if (prop != null)
                    {
                        headerMap[i] = prop;
                    }
                }

                // 2. Process Data Rows
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] values = ParseLineRobust(lines[i], separator);
                    var item = new T();

                    foreach (var mapping in headerMap)
                    {
                        int columnIndex = mapping.Key;
                        PropertyInfo prop = mapping.Value;

                        if (columnIndex < values.Length)
                        {
                            string stringValue = values[columnIndex].Trim();

                            try
                            {
                                // 3. Convert string value to the target property's type
                                //object typedValue = Convert.ChangeType(stringValue, prop.PropertyType, System.Globalization.CultureInfo.InvariantCulture);
                                //prop.SetValue(item, typedValue);

                                object? safeValue = string.IsNullOrEmpty(stringValue) ? null : ConvertCsvValue(stringValue, prop.PropertyType);
                                prop.SetValue(item, safeValue);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Warning: Could not convert value '{stringValue}' to type {prop.PropertyType.Name} for property {prop.Name}.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error setting property {prop.Name}: {ex.Message}");
                            }
                        }
                    }
                    records.Add(item);
                }
            }
            catch(Exception ex)
            {
                Debug.Print("Failed ReadFromCSV()");
            }
            return records;
        }

        public static object? ConvertCsvValue(string raw, Type targetType)
        {
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (string.IsNullOrEmpty(raw)) return null;
                targetType = Nullable.GetUnderlyingType(targetType) ?? targetType;
            }

            if (targetType.IsEnum)
            {
                return Enum.Parse(targetType, raw);
            }
            else if (targetType == typeof(string))
            {
                return raw;
            }
            else if (targetType == typeof(int))
            {
                return int.Parse(raw, CultureInfo.InvariantCulture);
            }
            else if (targetType == typeof(long))
            {
                return long.Parse(raw, CultureInfo.InvariantCulture);
            }
            else if (targetType == typeof(double))
            {
                return double.Parse(raw, CultureInfo.InvariantCulture);
            }
            else if (targetType == typeof(float))
            {
                return float.Parse(raw, CultureInfo.InvariantCulture);
            }
            else if (targetType == typeof(decimal))
            {
                return decimal.Parse(raw, CultureInfo.InvariantCulture);
            }
            else if (targetType == typeof(bool))
            {
                return bool.Parse(raw);
            }
            else if (targetType == typeof(DateTime))
            {
                return DateTime.Parse(raw, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }
            else if (targetType == typeof(DateTimeOffset))
            {
                return DateTimeOffset.Parse(raw, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }
            else if (targetType == typeof(Guid))
            {
                return Guid.Parse(raw);
            }
            else if (targetType == typeof(BigInteger))
            {
                return BigInteger.Parse(raw, CultureInfo.InvariantCulture);
            }
            // fallback, try system conversion
            return Convert.ChangeType(raw, targetType, CultureInfo.InvariantCulture);
        }
    }
}
