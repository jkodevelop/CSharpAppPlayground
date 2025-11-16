using AngleSharp.Dom;
using HtmlAgilityPack;
using MethodTimer;
using System.Diagnostics;

// [TO DOCUMENT]
// HtmlAgilityPack common APIs, note this library uses XPath for querying
// var nodes = node.SelectNodes("//a[@href]");
// HtmlNode node;
// node.Attributes["value"].Value // example <input value="...">
// node.InnerHtml

// sources:
// https://html-agility-pack.net/from-file

namespace CSharpAppPlayground.FilesFolders.Files
{
    public class HtmlAgilityPackParser
    {
        public string filePath = "";

        HtmlAgilityPack.HtmlDocument doc;

        private void LoadFile(string filePath)
        {
            if(this.filePath != filePath)
            {
                this.filePath = filePath;
                HtmlWeb web = new HtmlWeb();
                // Load the HTML file from the specified path
                doc = web.Load(filePath);
            }
        }

        public void Run(string filePath)
        {
            LoadFile(filePath);
            Parse();
        }

        public void Query(string filePath, string query)
        {
            LoadFile(filePath);
            try 
            {
                var nodes = doc.DocumentNode.SelectNodes(query);
                Debug.Print($"Query: {query}, Count: {nodes.Count}");
            } 
            catch (Exception ex)
            {
                Debug.Print($"HtmlAgilityPack Query Exception: {ex.Message}");
            }
        }

        /// <summary>
        /// Structure of Bookmarks.html
        //
        //  <dt>
        //    <h3>Folder Name</h3>
        //    <dl><p>
        //      <dt><a href="Url" ADD_DATE="unix epoch">Name URL encoded</a></dt>
        //    </p></dl>
        //  </dt>
        //
        //  Browser specific keys:
        //  Firefox => <a LAST_MODIFIED="unix epoch">
        // 
        /// </summary>
        /// <param name="doc"></param>
        public void Parse()
        {
            var nodes = doc.DocumentNode.SelectNodes("//dt[1]");

            foreach (var node in nodes)
            {
                var n = node;
                //Debug.Print($"-- HtmlAgilityPack InnertText: \n{node.InnerText}");

                var childrenDocs = node.ChildNodes;
                foreach (var child in childrenDocs)
                {
                    Debug.Print($"---- HtmlAgilityPack Child InnertText: \n{child.InnerText}");
                }
                continue;
            }
        }

        private void GetFolderName(HtmlNode node)
        {
            var h3Node = node.SelectSingleNode(".//h3");
            if (h3Node != null)
            {
                string folderName = h3Node.InnerText;
                Debug.Print($"Folder Name: {folderName}");
            }
        }

        private void GetBookmarkLinks(HtmlNode node)
        {
            var linkNodes = node.SelectNodes(".//a[@href]");
            if (linkNodes != null)
            {
                foreach (var linkNode in linkNodes)
                {
                    string url = linkNode.Attributes["href"].Value;
                    string name = System.Net.WebUtility.UrlDecode(linkNode.InnerText);
                    Debug.Print($"Bookmark: {name}, URL: {url}");
                }
            }
        }
    }
}
