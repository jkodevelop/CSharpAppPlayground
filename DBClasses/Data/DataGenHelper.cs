using CSharpAppPlayground.Classes;
using CSharpAppPlayground.DBClasses.Data.SQLbenchmark;
using Google.Protobuf.WellKnownTypes;
using System.Diagnostics;
using System.Numerics;

namespace CSharpAppPlayground.DBClasses.Data
{
    public class DataGenHelper
    {
        public List<RepoVidInsert> ConvertListVidsDataPostgresAndRepoDBList(List<VidsSQL> vids)
        {
            // for whatever reason Postgres RepoDB insertAll API complains about the DateTime object, they need UTC 
            // fixing error: Cannot write DateTime with Kind=Unspecified to PostgreSQL type 'timestamp with time zone', only UTC is supported
            List<RepoVidInsert> csvEntities = vids
                .Select(v => new RepoVidInsert
                {
                    filename = v.filename,
                    filesizebyte = ConvertBigIntegerToNullableLong(v.filesizebyte),
                    duration = v.duration,
                    metadatetime = (v.metadatetime != null ? DateTime.SpecifyKind(v.metadatetime.Value, DateTimeKind.Utc) : null),
                    width = v.width,
                    height = v.height
                })
                .ToList();
            return csvEntities;
        }

        public List<RepoVidInsert> ConvertListVidsData(List<VidsSQL> vids) 
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
            return csvEntities;
        }

        public bool GenCSVfileWithData(List<VidsSQL> vids, string filePath)
        {
            bool success = false;
            try
            {
                var csvEntities = ConvertListVidsData(vids);
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

        /// <summary>
        /// Convert BigInteger? into a DB-friendly value.
        /// Returns DBNull.Value when null or when the BigInteger cannot be represented as Int64.
        /// When in-range, returns a boxed Int64 (long) which is IConvertible and accepted by SqlParameter.
        /// </summary>
        public static object ConvertBigIntegerToDbValue(BigInteger? value)
        {
            if (!value.HasValue)
                return DBNull.Value;

            BigInteger v = value.Value;
            BigInteger max = long.MaxValue;
            BigInteger min = long.MinValue;

            if (v <= max && v >= min)
            {
                return (long)v;
            }
            else
            {
                // Out of range for BIGINT in SQL DB. Log and return NULL to avoid casting exceptions.
                Debug.Print($"BigInteger value {v} is outside Int64 range; inserting NULL instead.");
                return DBNull.Value;
            }
        }

        /// <summary>
        /// Convert BigInteger? to nullable long for use with DB insert objects.
        /// Returns null for missing or out-of-range values (maps to SQL NULL).
        /// </summary>
        public static long? ConvertBigIntegerToNullableLong(BigInteger? value)
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
