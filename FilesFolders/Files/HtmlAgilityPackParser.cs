using AngleSharp.Dom;
using HtmlAgilityPack;
using System.Diagnostics;
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

            //variable for each tag that could be impeding for displaying the correct hierarchy
            var metas = doc_lessTags.DocumentNode.SelectNodes("//meta");
            var titles = doc_lessTags.DocumentNode.SelectNodes("//title");
            var h1s = doc_lessTags.DocumentNode.SelectNodes("//h1");
            var dts = doc_lessTags.DocumentNode.SelectNodes("//dt");
            var ps = doc_lessTags.DocumentNode.SelectNodes("//p");
            var hrs = doc_lessTags.DocumentNode.SelectNodes("//hr");
            var dds = doc_lessTags.DocumentNode.SelectNodes("//dd");
            var aa = doc_lessTags.DocumentNode.SelectNodes("//a");
            var h3s = doc_lessTags.DocumentNode.SelectNodes("//h3");

            //delete all tags that could be impeding (comments too)
            var doctype = doc_lessTags.DocumentNode.SelectSingleNode("/comment()[starts-with(.,'<!DOCTYPE')]");
            if (doctype != null)
            {
                doctype.Remove();
            }
            
            var comments = doc_lessTags.DocumentNode.SelectSingleNode("//comment()");
            if (comments != null)
            {
                comments.Remove();
            }
            foreach (var meta in metas)
            {
                meta.Remove();
            }
            foreach (var title in titles)
            {
                title.Remove();
            }
            foreach (var h1 in h1s)
            {
                h1.Remove();
            }
            if (dts != null)
            {
                foreach (var dt in dts)
                {
                    if (!dt.HasChildNodes)
                    {
                        dt.ParentNode.RemoveChild(dt);
                        continue;
                    }

                    for (var i = dt.ChildNodes.Count - 1; i >= 0; i--)
                    {
                        var child = dt.ChildNodes[i];
                        dt.ParentNode.InsertAfter(child, dt);
                    }
                    dt.ParentNode.RemoveChild(dt);
                }
            }
            if (ps != null)
            {
                foreach (var p in ps)
                {
                    if (!p.HasChildNodes)
                    {
                        p.ParentNode.RemoveChild(p);
                        continue;
                    }

                    for (var i = p.ChildNodes.Count - 1; i >= 0; i--)
                    {
                        var child = p.ChildNodes[i];
                        p.ParentNode.InsertAfter(child, p);
                    }
                    p.ParentNode.RemoveChild(p);
                }
            }
            if (hrs != null)
            {
                foreach (var hr in hrs)
                {
                    if (!hr.HasChildNodes)
                    {
                        hr.ParentNode.RemoveChild(hr);
                        continue;
                    }

                    for (var i = hr.ChildNodes.Count - 1; i >= 0; i--)
                    {
                        var child = hr.ChildNodes[i];
                        hr.ParentNode.InsertAfter(child, hr);
                    }
                    hr.ParentNode.RemoveChild(hr);
                }
            }
            if (dds != null)
            {
                foreach (var dd in dds)
                {
                    if (!dd.HasChildNodes)
                    {
                        dd.ParentNode.RemoveChild(dd);
                        continue;
                    }

                    for (var i = dd.ChildNodes.Count - 1; i >= 0; i--)
                    {
                        var child = dd.ChildNodes[i];
                        dd.ParentNode.InsertAfter(child, dd);
                    }
                    dd.ParentNode.RemoveChild(dd);
                }
            }
            if(aa != null)
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

            // Printout the final html
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
