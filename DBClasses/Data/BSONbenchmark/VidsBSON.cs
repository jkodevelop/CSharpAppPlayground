namespace CSharpAppPlayground.DBClasses.Data.BSONbenchmark
{
    // This is used for Inserting without _id, so MongoDB will auto generate for the document
    public class VidsBSON : Vids
    {
        public static string collection = "Vids";
        public long? filesizebyte { get; set; }
    }
}
