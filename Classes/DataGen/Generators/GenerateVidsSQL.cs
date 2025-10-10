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
            if(count <= 1000)
                return SingleThread(count); // less than or equal to 1000, use single thread its faster
            else
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
            var partitionSize = 10000; // Adjust based on your needs
            var partitions = (count / partitionSize) + (count % partitionSize > 0 ? 1 : 0);
            
            var results = new ConcurrentBag<List<VidsSQL>>();
            
            Parallel.For(0, partitions, i =>
            {
                var localRandomGen = new RandomDataGenerator();
                var localRand = new Random(Guid.NewGuid().GetHashCode());
                var localList = new List<VidsSQL>();
                
                var start = i * partitionSize;
                var end = Math.Min(start + partitionSize, count);
                
                for (int j = start; j < end; j++)
                {
                    var vid = new VidsSQL
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

//
// SUMMARY
//
// Method 'ParallelThread' executed in 00:00:01.5520845 ms, GenerateVidsSQL.ParallelThread(1000000)
// Method 'SingleThread' executed in 00:00:01.4247937 ms, GenerateVidsSQL.SingleThread(1000000)
// Method 'SingleThread' executed in 00:00:15.1837723 ms, GenerateVidsSQL.SingleThread(10000000)
// Method 'ParallelThread' executed in 00:00:17.5056310 ms, GenerateVidsSQL.ParallelThread(10000000)
// After localized and partitioned Parallel
// Parition: 1000
// Method 'ParallelThread' executed in 00:00:06.7192088 ms, GenerateVidsSQL.ParallelThread(10000000)
// Parition: 500
// Method 'ParallelThread' executed in 00:00:06.7269346 ms, GenerateVidsSQL.ParallelThread(10000000)
// Parition: 10000
// Method 'ParallelThread' executed in 00:00:06.8805132 ms, GenerateVidsSQL.ParallelThread(10000000)
// Removing Parition logic, added 2 seconds to average with partition
// Method 'ParallelThread' executed in 00:00:08.7369940 ms, GenerateVidsSQL.ParallelThread(10000000)

// FULL List
//
// Parition: 10000
//Method 'ParallelThread' executed in 00:00:00.0172685 ms, GenerateVidsSQL.ParallelThread(1000)
//Method 'ParallelThread' executed in 00:00:00.0089994 ms, GenerateVidsSQL.ParallelThread(5000)
//Method 'ParallelThread' executed in 00:00:00.0230335 ms, GenerateVidsSQL.ParallelThread(10000)
//Method 'ParallelThread' executed in 00:00:00.1348448 ms, GenerateVidsSQL.ParallelThread(100000)
//Method 'ParallelThread' executed in 00:00:00.9340628 ms, GenerateVidsSQL.ParallelThread(1000000)
//Method 'ParallelThread' executed in 00:00:06.0004151 ms, GenerateVidsSQL.ParallelThread(10000000)
//
//Method 'SingleThread' executed in 00:00:00.0040979 ms, GenerateVidsSQL.SingleThread(1000)
//Method 'SingleThread' executed in 00:00:00.0087623 ms, GenerateVidsSQL.SingleThread(5000)
//Method 'SingleThread' executed in 00:00:00.0223416 ms, GenerateVidsSQL.SingleThread(10000)
//Method 'SingleThread' executed in 00:00:00.1881593 ms, GenerateVidsSQL.SingleThread(100000)
//Method 'SingleThread' executed in 00:00:01.2953029 ms, GenerateVidsSQL.SingleThread(1000000)
//Method 'SingleThread' executed in 00:00:10.8450782 ms, GenerateVidsSQL.SingleThread(10000000)
//
// Parition: 2000
//Method 'ParallelThread' executed in 00:00:00.0211852 ms, GenerateVidsSQL.ParallelThread(1000)
//Method 'ParallelThread' executed in 00:00:00.0058897 ms, GenerateVidsSQL.ParallelThread(5000)
//Method 'ParallelThread' executed in 00:00:00.0147914 ms, GenerateVidsSQL.ParallelThread(10000)
//Method 'ParallelThread' executed in 00:00:00.1512057 ms, GenerateVidsSQL.ParallelThread(100000)
//Method 'ParallelThread' executed in 00:00:00.8186177 ms, GenerateVidsSQL.ParallelThread(1000000)
//Method 'ParallelThread' executed in 00:00:06.4458419 ms, GenerateVidsSQL.ParallelThread(10000000)