namespace CSharpAppPlayground.DBClasses.Data
{
    public class Vids
    {
        public string filename { get; set; }
        public int? duration { get; set; } // duration in seconds
        public DateTime? metadatetime { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
    }
}
