using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

// usage:
// var rows = new List<VidSQL> { /* ... */ };
// SaveToCSV.SaveListToCsv(rows, @"C:\\temp\\vids.csv"); // default ':' separator

namespace CSharpAppPlayground.Classes.DataGen
{
    public class SaveToCSV
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
    }
}
