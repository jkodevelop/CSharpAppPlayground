namespace CSharpAppPlayground.DBClasses.Data.SQLbenchmark
{
    /// <summary>
    /// Simple POCO used for RepoDB insertion to reduce mapping overhead vs anonymous types.
    /// </summary>
    public class RepoVidInsert
    {
        public string filename { get; set; }
        public long? filesizebyte { get; set; }
        public int? duration { get; set; }
        public DateTime? metadatetime { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
    }
}
