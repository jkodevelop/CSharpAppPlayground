namespace CSharpAppPlayground.FilesFolders.Files
{
    public class BookmarkItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
        // public DateTime AddDate { get; set; }
        public List<BookmarkItem> Children { get; set; } = new();
    }
}
