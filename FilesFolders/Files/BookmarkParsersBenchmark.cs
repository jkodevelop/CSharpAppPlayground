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
        public List<BookmarkLibrary> Test_HtmlAgilityPackParser(string filePath)
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
            //Test_BookmarksManagerParser(filePath);
        }

        public void PrintResults(List<BookmarkLibrary> items)
        {
            foreach(var item in items)
            {
                PrintBookmarkItems(item, 0);
            }
        }

        public void PrintBookmarkItems(BookmarkLibrary item, int indentLevel)
        {
            //string indent = new string(' ', indentLevel * 2);
            //string output = $"{indent}Name: {item.Name}, Url: {item.Url}";

            //Debug.Print(output);
            //f.updateRichTextBoxMain(output);

            //foreach (var child in item.Children)
            //{
            //    PrintBookmarkItems(child, indentLevel + 1);
            //}
        }

        [Time("HtmlAgilityPack->Query()")]
        public int Test_HtmlAgilityPackParserQuery(string filePath, string query)
        {
            return htmlAgilityPackParser.Query(filePath, query);
        }

        [Time("AngleSharp->Query()")]
        public void Test_AngleSharpParserQuery(string filePath, string query)
        {
            // TODO
            angleSharpParsers.Query(filePath, query);
        }

        public void QueryTest(string filePath, string query)
        {
            int hapCount = Test_HtmlAgilityPackParserQuery(filePath, query);
            f.updateRichTextBoxMain($"HtmlAgilityPack:{query}, count:{hapCount}");

            Test_AngleSharpParserQuery(filePath, query);
        }

        // This will return a cleaner HTML file with unnecessary tags removed and return clean/structured html tree
        public void CleanHtml(string filePath)
        {
            bool success = htmlAgilityPackParser.CleanUpBookmarkFile(filePath, cleanedOutPath);
        }
    }
}

//sources:
// https://stackoverflow.com/questions/40300596/how-to-use-bookmarksmanager-chrome-to-get-bookmark-hierarchy
// https://www.nuget.org/packages/BookmarksManager