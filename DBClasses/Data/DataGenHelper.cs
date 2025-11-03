using CSharpAppPlayground.Classes;
using CSharpAppPlayground.DBClasses.Data.SQLbenchmark;
using System.Diagnostics;
using System.Numerics;

namespace CSharpAppPlayground.DBClasses.Data
{
    public class DataGenHelper
    {
        public bool GenCSVfileWithData(List<VidsSQL> vids, string filePath)
        {
            bool success = false;
            try
            {
                // TODO: compare performance for remapping, removing id
                // 1. using Select()
                List<RepoVidInsert> csvEntities = vids
                    .Select(v => new RepoVidInsert
                    {
                        filename = v.filename,
                        filesizebyte = ConvertBigIntegerToNullableLong(v.filesizebyte),
                        duration = v.duration,
                        metadatetime = v.metadatetime,
                        width = v.width,
                        height = v.height
                    })
                    .ToList();

                // 2. loop + create
                //var currentBatch = new List<RepoVidInsert>();
                //foreach (var v in vids)
                //{
                //    var entity = new RepoVidInsert
                //    {
                //        filename = v.filename,
                //        filesizebyte = ConvertBigIntegerToNullableLong(v.filesizebyte),
                //        duration = v.duration,
                //        metadatetime = v.metadatetime,
                //        width = v.width,
                //        height = v.height
                //    };
                //    currentBatch.Add(entity);
                //}
                CsvHandler.SaveListToCsv(csvEntities, filePath);
                success = true;
            }
            catch (Exception ex)
            {
                Debug.Print($"Error in GenCSVfileWithData: {ex.Message}");
                success = false;
            }
            return success;
        }

        public long? ConvertBigIntegerToNullableLong(BigInteger? value)
        {
            if (!value.HasValue)
                return null;

            BigInteger v = value.Value;
            BigInteger max = long.MaxValue;
            BigInteger min = long.MinValue;

            if (v <= max && v >= min)
            {
                return (long)v;
            }
            else
            {
                Debug.Print($"BigInteger value {v} is outside Int64 range; inserting NULL (as null) instead.");
                return null;
            }
        }
    }
}
