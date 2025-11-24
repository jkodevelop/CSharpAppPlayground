using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System.Diagnostics;

// NuGet Package: AngleSharp

// sources:
// https://sd.blackball.lv/en/articles/read/18989-csharp-parse-html-with-anglesharp

namespace CSharpAppPlayground.FilesFolders.Files
{
    public class AngleSharpParser
    {
        public string filePath = "";

        private void ProcessFolder(IElement node, FolderBookmark folder)
        {

        }

        public FolderBookmark ExtractFolderStructure(string filePath)
        {
            // TODO: implement extraction of folder structure
            string htmlContent = File.ReadAllText(filePath);
            var parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(htmlContent);

            IElement? root = document.QuerySelector("dl"); // get the root <dl> element
            FolderBookmark rootFolder = new FolderBookmark();
            rootFolder.Name = "Root";

            

            if (root != null)
            {

                var ll = root.QuerySelectorAll(":scope > a");


                // Start processing from the root element
                var links = root.QuerySelectorAll("a");
                rootFolder.Links = links.Select(link => new LinkBookmark
                {
                    Name = link.TextContent ?? "",
                    Url = link.GetAttribute("href") ?? ""
                }).ToList();

                root.GetElementsByTagName("a").ToList().ForEach(a =>
                {
                   Debug.Print($"Link: {a.TextContent}, Href: {a.GetAttribute("href")}");
                });

                //root.Children.ToList().ForEach(child =>
                //{
                //    if (child is IElement element)
                //    {
                //        Debug.Print($"Name: {child.TextContent} <{child.TagName}>");
                //    }
                //});

                Debug.Print("-------- anglesharp -------");
                Debug.Print(document.DocumentElement.OuterHtml);
            }

            return rootFolder;
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
