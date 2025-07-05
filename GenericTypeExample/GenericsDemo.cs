using System.Diagnostics;

namespace CSharpAppPlayground.GenericTypeExample
{
    public static class GenericsDemo
    {
        /// <summary>
        /// 
        /// This demonstrates a simple generic method that swaps two variables of any type.
        /// Swap<T> = allows the method to accept a type you specify at runtime, this is explicity defined by you.
        ///           Also allows for Compiler to infer the type from the arguments you pass in.
        /// (ref T a) = pass the reference of variable instead of value, T is a placeholder for the type you specify.
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static void Show()
        {
            int x = 10, y = 20;
            Debug.Print($"Before Swap: x = {x}, y = {y}");
            Swap(ref x, ref y); // Compiler can infer the type from the arguments
            Debug.Print($"After Swap: x = {x}, y = {y}");

            string str1 = "Hello", str2 = "World";
            Debug.Print($"Before Swap: str1 = {str1}, str2 = {str2}");
            Swap(ref str1, ref str2); // Compiler can infer the type from the arguments
            Debug.Print($"After Swap: str1 = {str1}, str2 = {str2}");

            float f1 = 1.5f, f2 = 2.5f;
            Debug.Print($"Before Swap: f1 = {f1}, f2 = {f2}");
            Swap<float>(ref f1, ref f2); // Explicitly specifying the type, but not necessary as compiler can infer it
            Debug.Print($"After Swap: f1 = {f1}, f2 = {f2}");
        }
    }
}
