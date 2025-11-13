using MethodTimer;

// source: https://webscraping.ai/faq/c/what-are-the-differences-between-htmlagilitypack-and-anglesharp-for-c-web-scraping

namespace CSharpAppPlayground.FilesFolders.Files
{
    public class BookmarkParsersBenchmark
    {
        HtmlAgilityPackParser htmlAgilityPackParser = new HtmlAgilityPackParser();
        AngleSharpParsers angleSharpParsers = new AngleSharpParsers();

        [Time("HtmlAgilityPack")]
        public void Test_HtmlAgilityPackParser(string filePath)
        {
            htmlAgilityPackParser.Run(filePath);
        }

        [Time("AngleSharp")]
        public void Test_AngleSharpParser(string filePath)
        {
            angleSharpParsers.Run(filePath);
        }

        public void RunBenchmarks(string filePath)
        {
            Test_HtmlAgilityPackParser(filePath);
            Test_AngleSharpParser(filePath);
        }
    }
}
