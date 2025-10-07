using Google.Protobuf.WellKnownTypes;
using System.Numerics;

namespace CSharpAppPlayground.DBClasses.Data.SQLbenchmark
{
    public class VidsSQL
    {
        private string tableName = "vids";

        public int id { get; set; }
        public string filename { get; set; }
        public BigInteger? filesizebyte { get; set; }
        public int? duration { get; set; } // duration in seconds
        public DateTime? metadatetime { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }

        public override string ToString()
        {
            return $"SQL Table: {tableName}";
        }
    }
}
