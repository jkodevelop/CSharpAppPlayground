using HtmlAgilityPack;

// sources:
// https://html-agility-pack.net/from-file

namespace CSharpAppPlayground.FilesFolders.Files
{
    public class HtmlAgilityPackParser
    {
        public string filePath = "";

        public void Run(string filePath)
        {
            this.filePath = filePath;

            HtmlWeb document = new HtmlWeb();

            // Load the HTML file from the specified path
            document.Load(filePath);
        }
    }
}
