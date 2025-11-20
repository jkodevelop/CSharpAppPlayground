namespace CSharpAppPlayground.FilesFolders.Files
{
    public class LinkBookmark
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class FolderBookmark
    {
        public string Name { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<LinkBookmark> Links { get; set; }
        public List<FolderBookmark> SubFolders { get; set; }
        public FolderBookmark()
        {
            Links = new List<LinkBookmark>();
            SubFolders = new List<FolderBookmark>();
        }
    }

    public class BookmarkLibrary
    {
        public void PrintTree()
        {
            //TODO: Implement PrintTree method
        }
    }
}
