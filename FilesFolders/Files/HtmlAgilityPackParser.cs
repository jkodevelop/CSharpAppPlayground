using AngleSharp.Dom;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net;
using System.Text.RegularExpressions;
using Windows.Media.AppBroadcasting;

// NuGet Package: HtmlAgilityPack

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

        public void PrintHtmlContent(HtmlAgilityPack.HtmlDocument d)
        {
            string htmlContent = d.DocumentNode.OuterHtml;
            Debug.Print(htmlContent);
        }

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

                // PrintHtmlContent(doc);
            }
        }

        public void RemoveElements(HtmlAgilityPack.HtmlDocument d, string key)
        {
            var elements = d.DocumentNode.SelectNodes(key);
            foreach (var e in elements)
            {
                e.Remove();
            }
        }

        public void RearrangeElements(HtmlAgilityPack.HtmlDocument d, string key)
        {
            var elements = d.DocumentNode.SelectNodes(key);
            if (elements != null)
            {
                foreach (var e in elements)
                {
                    if (!e.HasChildNodes)
                    {
                        e.ParentNode.RemoveChild(e);
                        continue;
                    }
                    for (var i = e.ChildNodes.Count - 1; i >= 0; i--)
                    {
                        var child = e.ChildNodes[i];
                        e.ParentNode.InsertAfter(child, e);
                    }
                    e.ParentNode.RemoveChild(e);
                }
            }
        }

        // source: https://stackoverflow.com/questions/40300596/how-to-use-bookmarksmanager-chrome-to-get-bookmark-hierarchy
        public bool CleanUpBookmarkFile(string filePath, string outPath)
        {
            StreamReader BookmarkDatei = new StreamReader(filePath);
            string content = BookmarkDatei.ReadToEnd();
            BookmarkDatei.Close();
            HtmlAgilityPack.HtmlDocument doc_lessTags = new HtmlAgilityPack.HtmlDocument();

            //deletes all DD Tags
            string DD = "(<DD>[a-zA-Z0-9]+[^<]+)"; //Regex-Pattern
            doc_lessTags.LoadHtml(Regex.Replace(content, DD, ""));

            string[] elementsToDelete = new string[] { "/comment()[starts-with(.,'<!DOCTYPE')]", "//comment()",
                "//meta", "//title", "//h1" };
            string[] elementsToRearrange = new string[] { "//dt","//p","//hr","//dd" };

            foreach(string key in elementsToDelete)
            {
                RemoveElements(doc_lessTags, key);
            }

            foreach(string key in elementsToRearrange)
            {
                RearrangeElements(doc_lessTags, key);
            }

            // clean up <a> tags - remove icon and icon_uri attributes, too much data not needed
            var aa = doc_lessTags.DocumentNode.SelectNodes("//a");
            if (aa != null)
            {
                var attributesToRemove = new[] { "icon", "icon_uri" }; // "icon_url" - not used
                foreach (var a in aa)
                {
                    foreach (var attrName in attributesToRemove)
                    {
                        if (a.Attributes[attrName] != null)
                            a.Attributes.Remove(attrName);
                    }
                }
            }

            // DEBUG: Printout the final html
            PrintHtmlContent(doc_lessTags);
            
            try
            {
                // 1. create the folders to the filePath
                Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(outPath)) ?? ".");
                // 2. save the cleaned html to a file
                doc_lessTags.Save(outPath);
            }
            catch(Exception ex)
            {
                Debug.Print($"HtmlAgilityPackParser.CleanUpBookmarkFile({outPath}): Exception saving cleaned file: {ex.Message}");
                return false;
            }
            return true;
        }

        public List<BookmarkLibrary> Run(string filePath)
        {
            // TODO
            LoadFile(filePath);
//             List<BookmarkLibrary> res = Parse();
            List<BookmarkLibrary> res = new List<BookmarkLibrary>();
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
        //public List<BookmarkLibrary> Parse()
        //{
        //   var dl = doc.DocumentNode.SelectSingleNode("//dl/p");
        //   var items = ParseDL(dl);
        //   return items;
        //}

        //public List<BookmarkLibrary> ParseDL(HtmlNode dlNode)
        //{
        //    List<BookmarkLibrary> items = new List<BookmarkLibrary>();
        //    var dtNodes = dlNode.SelectNodes("./dt");
        //    if (dtNodes != null)
        //    {
        //        foreach (var dtNode in dtNodes)
        //        {
        //            BookmarkLibrary item = new BookmarkLibrary();
        //            var h3Node = dtNode.SelectSingleNode("./h3");
        //            if (h3Node != null)
        //            {
        //                item.Name = h3Node.InnerText;
        //                var subDlNode = dtNode.SelectSingleNode("./dl/p"); // ./following-sibling::dl[1]
        //                if (subDlNode != null)
        //                {
        //                    item.Children = ParseDL(subDlNode);
        //                }
        //            }
        //            else
        //            {
        //                var aNode = dtNode.SelectSingleNode("./a[@href]");
        //                if (aNode != null)
        //                {
        //                    item.Name = System.Net.WebUtility.UrlDecode(aNode.InnerText);
        //                    item.Url = aNode.Attributes["href"].Value;
        //                }
        //            }
        //            items.Add(item);
        //        }
        //    }
        //    return items;
        //}

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
