using MethodTimer;

// source: https://webscraping.ai/faq/c/what-are-the-differences-between-htmlagilitypack-and-anglesharp-for-c-web-scraping

namespace CSharpAppPlayground.FilesFolders.Files
{
    public class BookmarkParsersBenchmark
    {
        HtmlAgilityPackParser htmlAgilityPackParser = new HtmlAgilityPackParser();
        AngleSharpParsers angleSharpParsers = new AngleSharpParsers();

        [Time("HtmlAgilityPack->Run()")]
        public void Test_HtmlAgilityPackParser(string filePath)
        {
            htmlAgilityPackParser.Run(filePath);
        }

        [Time("AngleSharp->Run()")]
        public void Test_AngleSharpParser(string filePath)
        {
            angleSharpParsers.Run(filePath);
        }

        public void RunBenchmarks(string filePath)
        {
            Test_HtmlAgilityPackParser(filePath);
            Test_AngleSharpParser(filePath);
        }

        [Time("HtmlAgilityPack->Query()")]
        public void Test_HtmlAgilityPackParserQuery(string filePath, string query)
        {
            htmlAgilityPackParser.Query(filePath, query);
        }

        [Time("AngleSharp->Query()")]
        public void Test_AngleSharpParserQuery(string filePath, string query)
        {
            angleSharpParsers.Query(filePath, query);
        }

        public void QueryTest(string filePath, string query)
        {
            htmlAgilityPackParser.Query(filePath, query);
            // angleSharpParsers.Query(filePath, query); // TODO
        }
    }
}
