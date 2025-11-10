using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

// usage:
// var rows = new List<VidSQL> { /* ... */ };
// SaveToCSV.SaveListToCsv(rows, @"C:\\temp\\vids.csv"); // default ':' separator

namespace CSharpAppPlayground.Classes
{
    public class CsvHandler
    {
        /// <summary>
        /// Saves a list of flat objects to a colon-separated CSV file.
        /// Only simple properties are included (primitives, string, decimal, DateTime, Guid, enum, and Nullable of these).
        /// </summary>
        /// <typeparam name="T">The item type (e.g., VidSQL).</typeparam>
        /// <param name="items">Items to serialize.</param>
        /// <param name="filePath">Destination file path.</param>
        /// <param name="includeHeader">Whether to include a header row.</param>
        /// <param name="separator">Column separator (default ':').</param>
        public static void SaveListToCsv<T>(IEnumerable<T> items, string filePath, bool includeHeader = true, char separator = ':') where T : class
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("File path must be provided", nameof(filePath));

            var itemType = typeof(T);
            var properties = GetSerializableProperties(itemType);

            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(filePath)) ?? ".");

            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
            using (var writer = new StreamWriter(stream))
            {
                if (includeHeader)
                {
                    var header = string.Join(separator, properties.Select(p => EscapeValue(p.Name, separator)));
                    writer.WriteLine(header);
                }

                foreach (var item in items)
                {
                    var values = properties.Select(p => EscapeValue(FormatValue(p.GetValue(item)), separator));
                    writer.WriteLine(string.Join(separator, values));
                }
            }
        }

        private static IReadOnlyList<PropertyInfo> GetSerializableProperties(Type type)
        {
            return type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && IsSimpleType(p.PropertyType))
                .ToArray();
        }

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
                   type == typeof(DateTime) ||
                   type == typeof(DateTimeOffset) ||
                   type == typeof(TimeSpan) ||
                   type == typeof(Guid);
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

        private static string EscapeValue(string? raw, char separator)
        {
            if (raw == null) return string.Empty;

            bool needsQuoting = raw.IndexOfAny(new[] { '\"', '\n', '\r', separator }) >= 0;
            string escaped = raw.Replace("\"", "\"\"");
            return needsQuoting ? "\"" + escaped + "\"" : escaped;
        }

        public static List<T> ReadListFromCsv<T>(string filePath, string separator = ":") where T : class{
            var list = new List<T>();
            var type = typeof(T);

            // Special handling for Vids type (case-insensitive)
            bool isVids = type.Name.Equals("Vids", StringComparison.OrdinalIgnoreCase);

            using (var reader = new StreamReader(filePath))
            {
                string? headerLine = reader.ReadLine();
                if (headerLine == null) return list;

                string[] headers = headerLine.Split(',');

                // Normalize header whitespace and keep mapping to property names
                var propMap = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
                foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    propMap[prop.Name] = prop;
                }

                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] values = ParseCsvLine(line, headers.Length);

                    // Create instance of T
                    var obj = Activator.CreateInstance(type);
                    if (obj == null) continue;

                    for (int i = 0; i < headers.Length && i < values.Length; i++)
                    {
                        string header = headers[i].Trim();
                        if (!propMap.TryGetValue(header, out var prop)) continue;

                        if (!prop.CanWrite) continue;

                        string rawValue = values[i];

                        try
                        {
                            object? safeValue = string.IsNullOrEmpty(rawValue) ? null : ConvertCsvValue(rawValue, prop.PropertyType);
                            prop.SetValue(obj, safeValue);
                        }
                        catch
                        {
                            // Could log or ignore unsuccessful conversion.
                        }
                    }

                    list.Add((T)obj);
                }
            }
            return list;

            // Helper to parse CSV lines with quotes (minimal, not full RFC 4180)
            static string[] ParseCsvLine(string line, int expectedColumns)
            {
                var result = new List<string>();
                bool inQuotes = false;
                int start = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '\"')
                    {
                        inQuotes = !inQuotes;
                    }
                    else if (line[i] == ',' && !inQuotes)
                    {
                        result.Add(UnescapeCsvValue(line.Substring(start, i - start)));
                        start = i + 1;
                    }
                }
                // Add last value
                result.Add(UnescapeCsvValue(line.Substring(start)));

                // Pad with blanks if fewer columns than headers
                while (result.Count < expectedColumns)
                    result.Add("");
                return result.ToArray();
            }

            static string UnescapeCsvValue(string raw)
            {
                raw = raw.Trim();
                if (raw.StartsWith("\"") && raw.EndsWith("\"") && raw.Length > 1)
                {
                    raw = raw.Substring(1, raw.Length - 2);
                    raw = raw.Replace("\"\"", "\"");
                }
                return raw;
            }

            static object? ConvertCsvValue(string raw, Type targetType)
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
                // fallback, try system conversion
                return Convert.ChangeType(raw, targetType, CultureInfo.InvariantCulture);
            }
        }
    }
}
