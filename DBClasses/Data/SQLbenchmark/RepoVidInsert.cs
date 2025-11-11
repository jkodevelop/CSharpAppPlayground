namespace CSharpAppPlayground.DBClasses.Data.SQLbenchmark
{
    /// <summary>
    /// Simple POCO used for RepoDB insertion to reduce mapping overhead vs anonymous types.
    /// </summary>
    public class RepoVidInsert : Vids
    {
        public long? filesizebyte { get; set; }
    }
}
