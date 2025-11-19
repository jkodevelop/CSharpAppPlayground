namespace CSharpAppPlayground.FilesFolders.Files
{
    public class BookmarkLink
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class BookmarkFolder
    {
        public string Name { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<BookmarkLink> Links { get; set; }
        public List<BookmarkFolder> SubFolders { get; set; }
        public BookmarkFolder()
        {
            Links = new List<BookmarkLink>();
            SubFolders = new List<BookmarkFolder>();
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
