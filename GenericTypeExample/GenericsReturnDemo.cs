using System.Diagnostics;

namespace CSharpAppPlayground.GenericTypeExample
{
    public class Product { public string Name { get; set; } }
    public static class GenericsReturnDemo
    {
        public static T GetFirstOrDefaultForEnumerable<T>(IEnumerable<T> items)
        {
            // Check if the collection is null or has no elements.
            if (items == null || !items.Any())
            {
                Debug.Print($"Collection of type '{typeof(T).Name}' is empty. Returning default value.");

                // The 'default(T)' keyword is magic. It returns:
                // - 0 for numeric types (int, double, etc.)
                // - false for bool
                // - null for reference types (string, class objects)
                return default(T);
            }
            // If we get here, the collection has items, so return the first one.
            return items.First();
        }

        public static void Show()
        {
            // --- Example with Integers ---
            List<int> numbers = new List<int> { 10, 20, 30 };
            int firstNumber = GetFirstOrDefaultForEnumerable(numbers);
            Debug.Print($"First number: {firstNumber}\n"); // Output: 10

            var emptyNumbers = new List<int>();
            int defaultNumber = GetFirstOrDefaultForEnumerable(emptyNumbers);
            Debug.Print($"Default for int: {defaultNumber}\n"); // Output: 0

            // --- Example with Strings ---
            string[] names = new[] { "Alice", "Bob", "Charlie" };
            string firstName = GetFirstOrDefaultForEnumerable(names);
            Debug.Print($"First name: {firstName}\n"); // Output: Alice

            var emptyNames = new string[0];
            string defaultName = GetFirstOrDefaultForEnumerable(emptyNames);
            // We use string interpolation to show that it is truly null.
            Debug.Print($"Default for string: {(defaultName == null ? "null" : defaultName)}\n"); // Output: null

            // --- Example with a Custom Class ---
            var products = new List<Product> { new Product { Name = "Chair" } };
            Product firstProduct = GetFirstOrDefaultForEnumerable(products);
            Debug.Print($"First product: {firstProduct.Name}\n"); // Output: Chair

            List<Product> emptyProducts = new List<Product>();
            Product defaultProduct = GetFirstOrDefaultForEnumerable<Product>(emptyProducts);
            Debug.Print($"Default for Product class: {(defaultProduct == null ? "null" : defaultProduct.Name)}\n"); // Output: null
        }
    }

}
