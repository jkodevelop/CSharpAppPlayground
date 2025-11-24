using HtmlAgilityPack;
using System.Diagnostics;
using System.Text.RegularExpressions;

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

/// Structure of Cleaned Bookmarks.html
/*
<dl>
  <h3>Folder Name</h3>
  <dl>
      <h3>Sub Folder Name</h3>
      <a href="Url" ADD_DATE="unix epoch">Name URL encoded</a>
      ...
  </dl>
  ...
  <a>
</dl>
*/

namespace CSharpAppPlayground.FilesFolders.Files
{
    public class HtmlAgilityPackParser
    {
        public string filePath = "";

        HtmlAgilityPack.HtmlDocument doc;

        // For debugging purposes
        public void PrintHtmlContent(HtmlAgilityPack.HtmlDocument d)
        {
            string htmlContent = d.DocumentNode.OuterHtml;
            Debug.Print("-------- HtmlAgilityPack -------\n");
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

        private void RemoveElements(HtmlAgilityPack.HtmlDocument d, string key)
        {
            var elements = d.DocumentNode.SelectNodes(key);
            foreach (var e in elements)
            {
                e.Remove();
            }
        }

        private void RearrangeElements(HtmlAgilityPack.HtmlDocument d, string key)
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
            // PrintHtmlContent(doc_lessTags);
            
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

        private void ProcessFolderNode(HtmlNode folderNode, FolderBookmark folder)
        {
            // process the folder content inside <dl>
            foreach (HtmlNode child in folderNode.ChildNodes)
            {
                if(child.GetType() == typeof(HtmlNode))
                {
                    if(child.Name == "h3")
                    {
                        //Debug.Print($"[Folder] FolderName: {folder.Name}, SubFolder Name: >{child.InnerText}<");
                        FolderBookmark subFolder = new FolderBookmark();
                        subFolder.Name = child.InnerText.Trim();
                        subFolder.AddDateUnixTimeSeconds = child.Attributes["add_date"]?.Value ?? "";
                        subFolder.ModifiedDateUnixTimeSeconds = child.Attributes["last_modified"]?.Value ?? "";
                        //if(child.Attributes["add_date"] != null)
                        //{
                        //    string addDateStr = child.Attributes["add_date"].Value;
                        //    subFolder.AddDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(addDateStr));
                        //}
                        //if(child.Attributes["last_modified"] != null)
                        //{
                        //    string modDateStr = child.Attributes["last_modified"].Value;
                        //    subFolder.ModifiedDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(modDateStr));
                        //}

                        // NOTE: .NextSibling() won't work, it returns the next text nodes with whitespace
                        ProcessFolderNode(child.SelectSingleNode("following-sibling::dl[1]"), subFolder); // <h3> next sibling is <dl>
                        folder.SubFolders.Add(subFolder);
                    }
                    else if(child.Name == "a")
                    {
                        string url = child.Attributes["href"].Value;
                        string name = System.Net.WebUtility.UrlDecode(child.InnerText.Trim());
                        //Debug.Print($"[LINK] FolderName: {folder.Name}, Bookmark: >{name}<, URL: {url}");
                        LinkBookmark link = new LinkBookmark();
                        link.Name = name;
                        link.Url = url;
                        link.AddDateUnixTimeSeconds = child.Attributes["add_date"]?.Value ?? "";
                        //if(child.Attributes["add_date"] != null)
                        //{
                        //    string addDateStr = child.Attributes["add_date"].Value;
                        //    link.AddDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(addDateStr));
                        //}
                        folder.Links.Add(link);
                    }
                }
            }
        }

        public FolderBookmark ExtractFolderStructure(string filePath)
        {
            // TODO
            LoadFile(filePath);

            FolderBookmark folderTreeRoot = new FolderBookmark();
            folderTreeRoot.Name = "Root";

            // first node is <dl>
            HtmlNode root = null;

            foreach (HtmlNode child in doc.DocumentNode.ChildNodes)
            {
                //Debug.Print("------------------------------------");
                //Debug.Print(child.GetType().ToString());
                //Debug.Print($"Child Node: {child.Name}, InnerText: {child.InnerText}");

                // get the first DL, this is the root
                if(child.GetType() == typeof(HtmlNode))
                {
                    root = child;
                }
            }

            if (root == null)
            {
                Debug.Print($"No root <DL> node found. Check the file {filePath}");
                return folderTreeRoot;
            }
            else 
            {
                // Debug.Print($"Child Node: {root.Name}, InnerText: {root.InnerText}");
                // Debug.Print($"Child Node: {root.Name}");
                ProcessFolderNode(root, folderTreeRoot);
            }
            return folderTreeRoot;
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
                    Debug.Print($"HtmlAgilityPackParser: Query: {q}, Count: {count}");
                }
                else
                    Debug.Print($"HtmlAgilityPackParser: Query: {q}=null");  
            } 
            catch (Exception ex)
            {
                Debug.Print($"HtmlAgilityPack Query Exception: {ex.Message}");
            }
            return count;
        }

        // use for benchmarking which lib gets all links fastest
        public HtmlNodeCollection GetAllLinks(string filePath)
        {
            LoadFile(filePath);
            HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//a");
            return links;
        }
    }
}
