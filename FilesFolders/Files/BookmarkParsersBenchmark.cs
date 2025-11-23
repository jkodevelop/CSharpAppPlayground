using CSharpAppPlayground.UIClasses;
using MethodTimer;
using HtmlAgilityPack;
using BookmarksManager;
using AngleSharp;
using System.Diagnostics;
using AngleSharp.Dom;

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
        private List<BookmarkLibrary> Test_HtmlAgilityPackParser(string filePath)
        {
            return htmlAgilityPackParser.Run(filePath);
        }

        [Time("AngleSharp->Run()")]
        private void Test_AngleSharpParser(string filePath)
        {
            angleSharpParsers.Run(filePath);
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

        #region Full Query Benchmark, includes file processing time

        [Time("HtmlAgilityPack->Query()")]
        private int Test_HtmlAgilityPackParserQuery(string filePath, string query)
        {
            return htmlAgilityPackParser.Query(filePath, query);
        }

        [Time("AngleSharp->Query()")]
        private int Test_AngleSharpParserQuery(string filePath, string query)
        {
            return angleSharpParsers.Query(filePath, query);
        }

        public void QueryTest(string filePath, string query)
        {
            int hapCount = Test_HtmlAgilityPackParserQuery(filePath, query);
            f.updateRichTextBoxMain($"HtmlAgilityPack:{query}, count:{hapCount}");

            int angCount = Test_AngleSharpParserQuery(filePath, query);
            f.updateRichTextBoxMain($"AngleSharp:{query}, count:{angCount}");
        }

        #endregion

        #region GetAllLinks <a> Benchmark

        [Time("HtmlAgilityPack->GetAllLinks()")]
        private HtmlNodeCollection Test_HtmlAgilityPackParserGetAllLinks(string filePath)
        {
            return htmlAgilityPackParser.GetAllLinks(filePath);
        }

        [Time("BookmarksManager->GetAllLinks()")]
        private List<BookmarkLink> Test_BookmarksManagerParserGetAllLinks(string fileContent)
        {
            return bookmarksManagerParser.GetAllLinks(fileContent);
        }

        [Time("AngleSharp->GetAllLinks()")]
        private IHtmlCollection<IElement> Test_AngleSharpParserGetAllLinks(string fileContent)
        {
            return angleSharpParsers.GetAllLinks(fileContent);
        }

        public void RunGetLinksBenchmark(string filePath)
        {
            HtmlNodeCollection htmlAgileResults = Test_HtmlAgilityPackParserGetAllLinks(filePath);
            Debug.Print($"HtmlAgilityPack <a> Count: {htmlAgileResults.Count}");

            string bookmarkHtml = File.ReadAllText(filePath);
            List<BookmarkLink> bookmarkResults = Test_BookmarksManagerParserGetAllLinks(bookmarkHtml);
            Debug.Print($"BookmarksManager <a> Count: {bookmarkResults.Count}");

            IHtmlCollection<IElement> angleSharpResults = Test_AngleSharpParserGetAllLinks(bookmarkHtml);
            Debug.Print($"AngleSharp <a> Count: {angleSharpResults.Count}");
        }

        #endregion

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