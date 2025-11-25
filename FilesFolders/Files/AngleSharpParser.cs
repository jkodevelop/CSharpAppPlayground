using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System.Diagnostics;

// NuGet Package: AngleSharp

// Summary: AngleSharp is not as fast as HtmlAgilityPack for any of it's functions.
// One advantage is it uses modern CSS selectors for querying HTML elements.
// One HUGE limit is the parsing automatically decodes HTML entities, and there is no way to disable this behavior.

// sources:
// https://sd.blackball.lv/en/articles/read/18989-csharp-parse-html-with-anglesharp

namespace CSharpAppPlayground.FilesFolders.Files
{
    public class AngleSharpParser
    {
        public string filePath = "";

        // Note: AngleSharp auto decodes HTML entities in .TextContent so use .InnerHtml instead
        private void ProcessFolder(IElement node, FolderBookmark folder)
        {
            // 1. get the links directly under this folder
            IHtmlCollection<IElement> links = node.QuerySelectorAll(":scope > a");
            folder.Links = links.Select(link => new LinkBookmark
            {
                Name = (link.InnerHtml ?? "").Trim(), // can't use link.TextContent, InnerHtml works to keep HTML entities
                Url = link.GetAttribute("href") ?? "",
                AddDateUnixTimeSeconds = link.GetAttribute("add_date") ?? ""
            }).ToList();

            IHtmlCollection<IElement> subFolders = node.QuerySelectorAll(":scope > h3");
            foreach (var subFolderElement in subFolders)
            {
                FolderBookmark subFolder = new FolderBookmark();
                subFolder.Name = (subFolderElement.InnerHtml ?? "").Trim(); // can't use link.TextContent, InnerHtml works to keep HTML entities
                subFolder.AddDateUnixTimeSeconds = subFolderElement.GetAttribute("add_date") ?? "";
                subFolder.ModifiedDateUnixTimeSeconds = subFolderElement.GetAttribute("last_modified") ?? "";

                // The corresponding <dl> element contains the contents of the folder
                IElement? dl = subFolderElement.NextElementSibling;
                if (dl != null && dl.TagName.ToLower() == "dl")
                {
                    ProcessFolder(dl, subFolder);
                }
                folder.SubFolders.Add(subFolder);
            }
        }

        public FolderBookmark ExtractFolderStructure(string filePath)
        {
            string htmlContent = File.ReadAllText(filePath);
            return ExtractFolderStructureContentStr(htmlContent);
        }

        public FolderBookmark ExtractFolderStructureContentStr(string htmlContent)
        {
            var parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(htmlContent);

            IElement? root = document.QuerySelector("dl"); // get the root <dl> element
            FolderBookmark rootFolder = new FolderBookmark();
            rootFolder.Name = "Root";

            if (root != null)
            {
                //PrintHtmlContent(document);

                ProcessFolder(root, rootFolder);
            }
            return rootFolder;
        }

        public void PrintHtmlContent(IHtmlDocument document)
        {
            Debug.Print("-------- AngleSharp -------\n");
            Debug.Print(document.DocumentElement.OuterHtml);
        }

        public int Query(string filePath, string query)
        {
            int count = 0;
            this.filePath = filePath;
            string htmlContent = File.ReadAllText(filePath);
            var parser = new HtmlParser();
            var document = parser.ParseDocument(htmlContent);

            try {
                var elements = document.QuerySelectorAll(query);
                count = elements.Length;
                Debug.Print($"AngleSharp: Query: {query}, Count: {elements.Length}");
            } 
            catch (Exception ex)
            {
                Debug.Print($"AngleSharp: Query Exception: {ex.Message}");
            }
            return count;
        }   

        public void QueryAlt(string filePath, string query)
        {
            this.filePath = filePath;
            string htmlContent = File.ReadAllText(filePath);
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);

            IDocument doc = context.OpenAsync(req => req.Content(htmlContent)).GetAwaiter().GetResult();
            try {
                var elements = doc.QuerySelectorAll(query);
                Debug.Print($"Query: {query}, Count: {elements.Length}");
            } 
            catch (Exception ex)
            {
                Debug.Print($"AngleSharp Query Exception: {ex.Message}");
            }
        }

        // use for benchmarking which lib gets all links fastest
        public IHtmlCollection<IElement> GetAllLinks(string bookmarkHtmlStr)
        {
            //var config = Configuration.Default.WithDefaultLoader();
            //var context = BrowsingContext.New(config);
            //IDocument doc = context.OpenAsync(req => req.Content(bookmarkHtmlStr)).GetAwaiter().GetResult();
            //List<IElement> links = doc.QuerySelectorAll("a").ToList();

            var parser = new HtmlParser();
            var document = parser.ParseDocument(bookmarkHtmlStr);
            // Query all <a> elements
            var links = document.QuerySelectorAll("a");

            return links;
        }
    }
}
