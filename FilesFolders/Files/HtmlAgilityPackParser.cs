using HtmlAgilityPack;
using System.Diagnostics;

// [TO DOCUMENT]
//
// HtmlAgilityPack common APIs, note this library uses XPath for querying
// var nodes = node.SelectNodes("//a[@href]");
//
// HtmlNode node;
// node.Attributes["value"].Value // example <input value="...">
// node.InnerHtml
//
// XPATH:
// (//DL/p)[1] - find the first occurence of <DL><p>
// MAYBE on the right track: (//DT/p)/DT[1] = 10

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
                //HtmlWeb web = new HtmlWeb();
                //// Load the HTML file from the specified path
                //doc = web.Load(filePath);
                //doc.OptionFixNestedTags = true;
                //doc.OptionAutoCloseOnEnd = true;
                //doc.OptionCheckSyntax = false;
                //doc.OptionWriteEmptyNodes = true;

                doc = new HtmlAgilityPack.HtmlDocument();
                doc.Load(filePath);

                string htmlContent = doc.DocumentNode.OuterHtml;
                // Debug.Print(htmlContent);
            }
        }

        public List<BookmarkItem> Run(string filePath)
        {
            LoadFile(filePath);
            List<BookmarkItem> res = Parse();
            return res;
        }

        public int Query(string filePath, string query)
        {
            string q = query.ToLower();
            LoadFile(filePath);

            int count = 0;
            try 
            {
                var nodes = doc.DocumentNode.SelectNodes(q);
                if (nodes != null)
                {
                    count = nodes.Count;
                    Debug.Print($"Query: {q}, Count: {count}");
                }
                else
                    Debug.Print($"Query: {q}=null");  
            } 
            catch (Exception ex)
            {
                Debug.Print($"HtmlAgilityPack Query Exception: {ex.Message}");
            }
            return count;
        }
        public List<BookmarkItem> Parse()
        {
           var dl = doc.DocumentNode.SelectSingleNode("//dl/p");
           var items = ParseDL(dl);
           return items;
        }

        public List<BookmarkItem> ParseDL(HtmlNode dlNode)
        {
            List<BookmarkItem> items = new List<BookmarkItem>();
            var dtNodes = dlNode.SelectNodes("./dt");
            if (dtNodes != null)
            {
                foreach (var dtNode in dtNodes)
                {
                    BookmarkItem item = new BookmarkItem();
                    var h3Node = dtNode.SelectSingleNode("./h3");
                    if (h3Node != null)
                    {
                        item.Name = h3Node.InnerText;
                        var subDlNode = dtNode.SelectSingleNode("./dl/p"); // ./following-sibling::dl[1]
                        if (subDlNode != null)
                        {
                            item.Children = ParseDL(subDlNode);
                        }
                    }
                    else
                    {
                        var aNode = dtNode.SelectSingleNode("./a[@href]");
                        if (aNode != null)
                        {
                            item.Name = System.Net.WebUtility.UrlDecode(aNode.InnerText);
                            item.Url = aNode.Attributes["href"].Value;
                        }
                    }
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// Structure of Bookmarks.html
        //
        //  <!DOCTYPE NETSCAPE-Bookmark-file-1>
        //  <dt>
        //    <h3>Folder Name</h3>
        //    <dl><p>
        //      <dt><h3>Sub Folder Name</h3>
        //      <dt><a href="Url" ADD_DATE="unix epoch">Name URL encoded</a></dt>
        //      ...
        //    </dl>
        //  </dt>
        //
        //  Browser specific keys:
        //  Firefox => <a LAST_MODIFIED="unix epoch">
        // 
        /// </summary>
        /// <param name="doc"></param>
        public void ParseWIP()
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
