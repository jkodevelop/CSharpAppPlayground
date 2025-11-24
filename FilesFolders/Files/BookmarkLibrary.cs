namespace CSharpAppPlayground.FilesFolders.Files
{
    public class LinkBookmark
    {
        public string? Name { get; set; }
        public string Url { get; set; } = "";
        public string AddDateUnixTimeSeconds { get; set; }
        public DateTimeOffset? AddDate { get; set; } // use string instead since its all UNIXTIMESECONDS
        // public DateTimeOffset ModifiedDate { get; set; } // exported bookmarks don't have modified date for links
    }

    public class FolderBookmark
    {
        public string? Name { get; set; }
        public string AddDateUnixTimeSeconds { get; set; } = "";
        public DateTimeOffset? AddDate { get; set; }
        public string ModifiedDateUnixTimeSeconds { get; set; } = "";
        public DateTimeOffset? ModifiedDate { get; set; }
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
