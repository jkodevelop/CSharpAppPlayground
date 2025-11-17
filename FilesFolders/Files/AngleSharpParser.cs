using AngleSharp;
using AngleSharp.Dom;
using System.Diagnostics;

// NuGet Package: AngleSharp

// sources:
// https://sd.blackball.lv/en/articles/read/18989-csharp-parse-html-with-anglesharp

namespace CSharpAppPlayground.FilesFolders.Files
{
    public class AngleSharpParsers
    {
        public string filePath = "";

        public void Run(string filePath)
        {
            this.filePath = filePath;
            string htmlContent = File.ReadAllText(filePath);
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);

            IDocument doc = context.OpenAsync(req => req.Content(htmlContent)).GetAwaiter().GetResult();

            // Example: Extracting info from IDocument
            Debug.Print($"Document Title: {doc.Title}");

            // Or query elements
            foreach (var element in doc.QuerySelectorAll("p"))
            {
                //Debug.Print($"Paragraph Text: {element.TextContent}");
            }
        }

        public void Query(string filePath, string query)
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
    }
}
