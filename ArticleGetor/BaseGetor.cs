using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HtmlAgilityPack;

namespace RSS2KINDLE
{
    public abstract class BaseGetor
    {
        protected string GetValidFileName(string rawFileName)
        {
            var fileName = new StringBuilder(rawFileName);
            var invalidFileName = new[] { '?', '\\', '*', '"', '<', '>', '|', '/', '.', ':', '+' };
            foreach (var chr in invalidFileName)
            {
                fileName.Replace(chr, '_');
            }
            return fileName.ToString();
        }
        protected void rmClassAtt(HtmlAgilityPack.HtmlNode node)
        {
            if (node.HasChildNodes)
            {
                foreach (var item in node.ChildNodes)
                {
                    if (item.Attributes["class"] != null)
                    {
                        item.Attributes["class"].Remove();
                    }
                    rmClassAtt(item);
                }
            }
            else
            {
                return;
            }
        }
        protected void rmHerfAtt(HtmlAgilityPack.HtmlNode node)
        {
            if (node.HasChildNodes)
            {
                foreach (var item in node.ChildNodes)
                {
                    if (item.Attributes["href"] != null)
                    {
                        item.Attributes["href"].Remove();
                    }
                    rmClassAtt(item);
                }
            }
            else
            {
                return;
            }
        }
        protected void rmAllAttFromDiv(HtmlAgilityPack.HtmlNode node)
        {
            if (node.HasChildNodes)
            {
                foreach (var item in node.ChildNodes)
                {
                    if (item.Name.ToLower() == "div")
                    {
                        while (item.HasAttributes)
                        {
                            item.Attributes[0].Remove();
                        }
                    }
                    rmAllAttFromDiv(item);
                }
            }
            else
            {
                return;
            }
        }


        protected string rmImgNode(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nodes = doc.DocumentNode.SelectNodes("//img");
            if (nodes!=null)
            {
                foreach (var n in nodes)
                {
                    n.Remove();
                }
            }
            return doc.DocumentNode.OuterHtml;
        }
        protected string rmStyle(string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            if(doc.DocumentNode.Descendants("style")!=null)
            {
                foreach (var style in doc.DocumentNode.Descendants("style").ToArray())
                {
                    style.Remove();
                }
            }
            return doc.DocumentNode.OuterHtml;
        }
        protected string rmScript(string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            if (doc.DocumentNode.Descendants("script") != null)
            {
                foreach (var script in doc.DocumentNode.Descendants("script").ToArray())
                {
                    script.Remove();
                }
            }
            return doc.DocumentNode.OuterHtml;
        }

        public virtual string SaveAsHtml(string title, string oHtml, string basePath)
        {
            string f = GetValidFileName(title);
            File.WriteAllText(Path.Combine(basePath, f + ".html"), oHtml, Encoding.UTF8);
            return f + ".html";
        }
        public virtual string SaveImage(string title, string oHtml, string basePath)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(oHtml);

            var nodes = doc.DocumentNode.SelectNodes("//img");
            if (nodes == null)
            {
                return doc.DocumentNode.OuterHtml;
            }

            int i = 0;
            foreach (var item in nodes)
            {
                try
                {
                    string image = item.Attributes["src"].Value;
                    string dir = GetValidFileName(title);
                    dir = Path.Combine(basePath, dir);

                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    Utility.DownloadImage(image, dir + "\\" + ++i + ".jpg");
                    item.Attributes["src"].Value = dir + "\\" + i + ".jpg";
                }
                catch (Exception err)
                {
                    continue;
                }
            }
            return doc.DocumentNode.OuterHtml.ToString();
        }
    }
}
