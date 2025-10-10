using System.Numerics;

namespace CSharpAppPlayground.Classes.DataGen
{
    public class RandomDataGenerator
    {
        private readonly Random _random = new();
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
        public int RandomInt(int min, int max)
        {
            return _random.Next(min, max);
        }
        public double RandomDouble(double min, double max)
        {
            return _random.NextDouble() * (max - min) + min;
        }
        public DateTime RandomDate(DateTime start, DateTime end)
        {
            int range = (end - start).Days;
            return start.AddDays(_random.Next(range));
        }

        /// <summary>
        /// Generates a random file size in bytes as a BigInteger
        /// </summary>
        /// <param name="minBytes">Minimum file size in bytes (default: 0)</param>
        /// <param name="maxBytes">Maximum file size in bytes (default: very large number)</param>
        /// <returns>Random BigInteger representing file size in bytes</returns>
        public BigInteger RandomFileSize(BigInteger? minBytes = null, BigInteger? maxBytes = null)
        {
            // Set default values
            BigInteger min = minBytes ?? 0;
            BigInteger max = maxBytes ?? BigInteger.Pow(2, 1024); // 2^1024 is a very large number
            
            if (min < 0)
                throw new ArgumentException("Minimum bytes cannot be negative", nameof(minBytes));
            
            if (max < min)
                throw new ArgumentException("Maximum bytes cannot be less than minimum bytes", nameof(maxBytes));

            if (min == max)
                return min;

            // Calculate the range
            BigInteger range = max - min;
            
            // Generate random bytes for the BigInteger
            byte[] randomBytes = new byte[32]; // 256 bits should be sufficient for most cases
            _random.NextBytes(randomBytes);
            
            // Create BigInteger from random bytes and ensure it's positive
            BigInteger randomValue = new BigInteger(randomBytes);
            if (randomValue < 0)
                randomValue = -randomValue;
            
            // Scale the random value to fit within the specified range
            if (range > 0)
            {
                randomValue = randomValue % range;
            }
            
            return min + randomValue;
        }

        public T RandomElement<T>(T[] array) where T : notnull
        {
            return array[_random.Next(array.Length)];
        }
    }
}
