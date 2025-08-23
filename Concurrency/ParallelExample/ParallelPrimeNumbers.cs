using CSharpAppPlayground.UIClasses;
using System.Collections.Concurrent;
using System.Diagnostics;
using MethodTimer;

namespace CSharpAppPlayground.Concurrency.ParallelExample
{
    public class ParallelPrimeNumbers : UIFormRichTextBoxHelper
    {
        protected int limit = 20000000;

        // private Form f;
        public ParallelPrimeNumbers(Form _f)
        {
            f = _f;
        }

        [Time("Prime Numbers Parallel Example")]
        public void Run()
        {
            Debug.Print("Finding prime numbers in a large list using Parallel.ForEach...");
            // Generate a large list of numbers
            IList<int> numbers = Enumerable.Range(0, limit).ToList();
            // Find prime numbers using parallel processing
            IList<int> primeNumbers = GetPrimeListWithParallel(numbers);
            // Display results
            this.RichTextbox($"Found {primeNumbers.Count} prime numbers between 1 and {limit}.", 
                Color.Blue, true);
        }
        private IList<int> GetPrimeListWithParallel(IList<int> numbers)
        {
            var primeNumbers = new ConcurrentBag<int>();
            Parallel.ForEach(numbers, number =>
            {
                if (IsPrime(number))
                {
                    primeNumbers.Add(number);
                }
            });
            return primeNumbers.ToList();
        }

        private bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int divisor = 2; divisor <= Math.Sqrt(number); divisor++)
            {
                if (number % divisor == 0) return false;
            }
            return true;
        }

        [Time("Prime Numbers Not-Parallel Example")]
        public void Compare()
        {
            Debug.Print("Finding prime numbers in a large list using Parallel.ForEach...");
            // Generate a large list of numbers
            IList<int> numbers = Enumerable.Range(0, limit).ToList();
            // Find prime numbers using parallel processing
            IList<int> primeNumbers = GetPrimeListWithoutParallel(numbers);
            // Display results
            this.RichTextbox($"Found {primeNumbers.Count} prime numbers between 1 and {limit}.", 
                Color.Green, true);
        }

        private IList<int> GetPrimeListWithoutParallel(IList<int> numbers)
        {
            return numbers.Where(IsPrime).ToList();
        }
    }
}
