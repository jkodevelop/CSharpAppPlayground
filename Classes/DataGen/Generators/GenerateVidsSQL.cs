using CSharpAppPlayground.DBClasses.Data.SQLbenchmark;
using MethodTimer;
using System.Collections.Concurrent;

namespace CSharpAppPlayground.Classes.DataGen.Generators
{
    public class GenerateVidsSQL : IDataGenerator<VidsSQL>
    {
        private static int[] randHeights, randWidths;

        public GenerateVidsSQL()
        {
            // randHeights = [ 144, 240, 360, 480, 720, 1080, 1440, 2160, 4320 ]; // Common video heights (p)
            // randWidths = [ 256, 426, 640, 854, 1280, 1920, 2560, 3840, 7680 ]; // Common video widths (p)
            randHeights = [480, 720, 1080]; // Common video heights (p)
            randWidths = [640, 1280, 1920]; // Common video widths (p)
        }

        public List<VidsSQL> GenerateData(int count)
        {
            //return SingleThread(count);
            return ParallelThread(count);
        }

        [Time("GenerateVidsSQL.SingleThread({count})")]
        protected List<VidsSQL> SingleThread(int count)
        {
            RandomDataGenerator randomGen = new RandomDataGenerator();
            Random rand = new Random();

            List<VidsSQL> dataList = new List<VidsSQL>();
            for (int i = 0; i < count; i++)
            {
                VidsSQL vid = new VidsSQL
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

        [Time("GenerateVidsSQL.ParallelThread({count})")]
        protected List<VidsSQL> ParallelThread(int count)
        {
            RandomDataGenerator randomGen = new RandomDataGenerator();
            ConcurrentBag<VidsSQL> dataBag = new ConcurrentBag<VidsSQL>();

            Parallel.For(0, count, i =>
            {
                // Each thread gets its own Random instance to avoid thread safety issues
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                
                VidsSQL vid = new VidsSQL
                {
                    filename = randomGen.RandomString(rand.Next(10, 45)) + ".mp4",
                    filesizebyte = randomGen.RandomFileSize(new System.Numerics.BigInteger(1_000_000), new System.Numerics.BigInteger(10_000_000_000)), // Between ~1MB and ~10GB
                    duration = randomGen.RandomInt(30, 7200), // Between 30 seconds and 2 hours
                    metadatetime = randomGen.RandomDate(new DateTime(2000, 1, 1), DateTime.Now),
                    height = randomGen.RandomElement<int>(randHeights),
                    width = randomGen.RandomElement<int>(randWidths)
                };

                // Thread-safe addition using ConcurrentBag
                dataBag.Add(vid);
            });

            // Convert ConcurrentBag to List for return
            return dataBag.ToList();
        }


        // performance test method
        public void TestPerformance()
        {
            //_ = GenerateData(1000); // Generate 1000 records for testing
            //_ = GenerateData(5000); // Generate 5000 records for testing
            //_ = GenerateData(10000); // Generate 10000 records for testing
            //_ = GenerateData(100000); // Generate 100000 records for testing
            //_ = GenerateData(1000000); // Generate 1 million records for testing
            _ = GenerateData(10000000); // Generate 10 million records for testing
        }
    }
}

// summary
// Method 'ParallelThread' executed in 00:00:01.5520845 ms, GenerateVidsSQL.ParallelThread(1000000)
// Method 'SingleThread' executed in 00:00:01.4247937 ms, GenerateVidsSQL.SingleThread(1000000)
// Method 'SingleThread' executed in 00:00:15.1837723 ms, GenerateVidsSQL.SingleThread(10000000)
// Method 'ParallelThread' executed in 00:00:17.5056310 ms, GenerateVidsSQL.ParallelThread(10000000)