using CSharpAppPlayground.UIClasses;
using MethodTimer;
using System.Diagnostics;

// source: https://webscraping.ai/faq/c/what-are-the-differences-between-htmlagilitypack-and-anglesharp-for-c-web-scraping

namespace CSharpAppPlayground.FilesFolders.Files
{
    public class BookmarkParsersBenchmark
    {
        HtmlAgilityPackParser htmlAgilityPackParser = new HtmlAgilityPackParser();
        AngleSharpParser angleSharpParsers = new AngleSharpParser();
        BookmarksManagerParser bookmarksManagerParser = new BookmarksManagerParser();
        public FormWithRichText f { get; set; }

        private string cleanedOutPath = @".\testdata\bookmarks\cleanedBookmark.html";

        public string GetCleanedOutPath()
        {
            return cleanedOutPath;
        }

        public BookmarkParsersBenchmark(FormWithRichText _f)
        {
            f = _f;
        }

        [Time("HtmlAgilityPack->Run()")]
        public List<BookmarkItem> Test_HtmlAgilityPackParser(string filePath)
        {
            return htmlAgilityPackParser.Run(filePath);
        }

        [Time("AngleSharp->Run()")]
        public void Test_AngleSharpParser(string filePath)
        {
            angleSharpParsers.Run(filePath);
        }

        [Time("BookmarksManager->Run()")]
        public void Test_BookmarksManagerParser(string filePath)
        {
            bookmarksManagerParser.Run(filePath);
        }

        public void RunBenchmarks(string filePath)
        {
            // HTML Agility Pack
            //List<BookmarkItem> bk = Test_HtmlAgilityPackParser(filePath);
            //PrintResults(bk);

            // AngleSharp
            //Test_AngleSharpParser(filePath);

            // BookmarksManager
            Test_BookmarksManagerParser(filePath);
        }

        public void PrintResults(List<BookmarkItem> items)
        {
            foreach(var item in items)
            {
                PrintBookmarkItems(item, 0);
            }
        }

        public void PrintBookmarkItems(BookmarkItem item, int indentLevel)
        {
            string indent = new string(' ', indentLevel * 2);
            string output = $"{indent}Name: {item.Name}, Url: {item.Url}";

            Debug.Print(output);
            f.updateRichTextBoxMain(output);

            foreach (var child in item.Children)
            {
                PrintBookmarkItems(child, indentLevel + 1);
            }
        }

        [Time("HtmlAgilityPack->Query()")]
        public void Test_HtmlAgilityPackParserQuery(string filePath, string query)
        {
            htmlAgilityPackParser.Query(filePath, query);
        }

        [Time("AngleSharp->Query()")]
        public void Test_AngleSharpParserQuery(string filePath, string query)
        {
            // TODO
            angleSharpParsers.Query(filePath, query);
        }

        public void QueryTest(string filePath, string query)
        {
            int htmlAgileQueryCount = htmlAgilityPackParser.Query(filePath, query);
            f.updateRichTextBoxMain($"HtmlAgilityPack:{query}, count:{htmlAgileQueryCount}");

            // angleSharpParsers.Query(filePath, query); // TODO
            
        }

        public void CleanHtml(string filePath)
        {
            bool success = htmlAgilityPackParser.CleanUpBookmarkFile(filePath, cleanedOutPath);
        }
    }
}

//sources:
// https://stackoverflow.com/questions/40300596/how-to-use-bookmarksmanager-chrome-to-get-bookmark-hierarchy
// https://www.nuget.org/packages/BookmarksManager