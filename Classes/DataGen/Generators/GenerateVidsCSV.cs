using CSharpAppPlayground.DBClasses.Data;
using CSharpAppPlayground.DBClasses.Data.SQLbenchmark;
using MethodTimer;
using System.Collections.Concurrent;

namespace CSharpAppPlayground.Classes.DataGen.Generators
{
    public class GenerateVidsCSV : IDataGenerator<VidsCSV>
    {
        private static int[] randHeights, randWidths;

        public GenerateVidsCSV()
        {
            // randHeights = [ 144, 240, 360, 480, 720, 1080, 1440, 2160, 4320 ]; // Common video heights (p)
            // randWidths = [ 256, 426, 640, 854, 1280, 1920, 2560, 3840, 7680 ]; // Common video widths (p)
            randHeights = [480, 720, 1080]; // Common video heights (p)
            randWidths = [640, 1280, 1920]; // Common video widths (p)
        }

        public List<VidsCSV> GenerateData(int count)
        {
            if (count <= 1000)
                return SingleThread(count); // less than or equal to 1000, use single thread its faster
            else
                return ParallelThread(count);
        }

        [Time("GenerateVidsCSV.SingleThread({count})")]
        protected List<VidsCSV> SingleThread(int count)
        {
            RandomDataGenerator randomGen = new RandomDataGenerator();
            Random rand = new Random();

            List<VidsCSV> dataList = new List<VidsCSV>();
            for (int i = 0; i < count; i++)
            {
                VidsCSV vid = new VidsCSV
                {
                    filename = randomGen.RandomString(rand.Next(10, 45)) + ".mp4",
                    filesizebyte = randomGen.RandomFileSize(new System.Numerics.BigInteger(1_000_000), new System.Numerics.BigInteger(10_000_000_000)), // Between ~1MB and ~10GB
                    duration = randomGen.RandomInt(30, 7200), // Between 30 seconds and 2 hours
                    metadatetime = randomGen.RandomDate(new DateTime(2000, 1, 1), DateTime.Now),
                    height = randomGen.RandomElement<int>(randHeights),
                    width = randomGen.RandomElement<int>(randWidths)
                };
                dataList.Add(vid);
            }
            return dataList;
        }

        [Time("GenerateVidsCSV.ParallelThread({count})")]
        protected List<VidsCSV> ParallelThread(int count)
        {
            var partitionSize = 10000; // Adjust based on your needs
            var partitions = (count / partitionSize) + (count % partitionSize > 0 ? 1 : 0);

            var results = new ConcurrentBag<List<VidsCSV>>();

            Parallel.For(0, partitions, i =>
            {
                var localRandomGen = new RandomDataGenerator();
                var localRand = new Random(Guid.NewGuid().GetHashCode());
                var localList = new List<VidsCSV>();

                var start = i * partitionSize;
                var end = Math.Min(start + partitionSize, count);

                for (int j = start; j < end; j++)
                {
                    var vid = new VidsCSV
                    {
                        filename = localRandomGen.RandomString(localRand.Next(10, 45)) + ".mp4",
                        filesizebyte = localRandomGen.RandomFileSize(new System.Numerics.BigInteger(1_000_000), new System.Numerics.BigInteger(10_000_000_000)),
                        duration = localRandomGen.RandomInt(30, 7200),
                        metadatetime = localRandomGen.RandomDate(new DateTime(2000, 1, 1), DateTime.Now),
                        height = localRandomGen.RandomElement<int>(randHeights),
                        width = localRandomGen.RandomElement<int>(randWidths)
                    };
                    localList.Add(vid);
                }
                results.Add(localList);
            });

            return results.SelectMany(x => x).ToList();
        }


        // performance test method
        public void TestPerformance()
        {
            _ = GenerateData(1000); // Generate 1000 records for testing
            _ = GenerateData(5000); // Generate 5000 records for testing
            _ = GenerateData(10000); // Generate 10000 records for testing
            _ = GenerateData(100000); // Generate 100000 records for testing
            _ = GenerateData(1000000); // Generate 1 million records for testing
            _ = GenerateData(10000000); // Generate 10 million records for testing
        }
    }
}
