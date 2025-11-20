using BookmarksManager;
using System.Diagnostics;

// NuGet Package: BookmarksManager
// sources:
// https://github.com/Dissimilis/BookmarksManager

// SUMMARY: this is not useful cause it returns a flat list, tree structure has to be manually recalculated.

namespace CSharpAppPlayground.FilesFolders.Files
{
    public class BookmarksManagerParser
    {
        NetscapeBookmarksReader r = new NetscapeBookmarksReader();

        public void Run(string filePath)
        {
            // option 1: 
            string bookmarkHtml = File.ReadAllText(filePath);
            var bookmarks = r.Read(bookmarkHtml);
            foreach (BookmarkLink b in bookmarks.AllLinks)
            {
                Debug.Print($"AllLinks() => Url: {b.Url}; Title: {b.Title}");
            }
            
            // option 2:
            using (var file = File.OpenRead(filePath))
            {
                //supports encoding detection when reading from stream
                var bk = r.Read(file);

                bk.AllFolders.ToList().ForEach((f) => {
                    Debug.Print($"Folder: {f.Title}");

                    f.AllItems.ToList().ForEach((i) => {
                        Debug.Print($"\tItem Type: {i.GetType().Name}, Title: {i.Title}");
                    });

                    f.AllFolders.ToList().ForEach((sf) =>
                    {
                        Debug.Print($"\tSubFolder: {sf.Title}");
                    });

                    f.GetAllItems<BookmarkLink>().ToList().ForEach((l) =>
                    {
                        Debug.Print($"\tAll Items - Link: {l.Title}, Url: {l.Url}");
                    });
                });

                //// A)
                //foreach (var b in bk.AllLinks.Where(l => l.LastVisit < DateTime.Today)) { }

                //// B)
                //foreach (var b in bk.AllLinks)
                //{
                //    Debug.Print($"Type: {b.GetType().Name}, Url: {b.Url}; Title: {b.Title}");
                //}
            }
        }

        // use for benchmarking which lib gets all links fastest
        public List<BookmarkLink> GetAllLinks(string bookmarkHtmlStr)
        {
            BookmarkFolder bk = r.Read(bookmarkHtmlStr);
            return bk.AllLinks.ToList();
        }
    }
}
