using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Net;

namespace RSS2KINDLE
{
    public class Utility
    {
        public static string GetHtml(Uri uri)
        {
            var req = ((HttpWebRequest)(WebRequest.Create(uri)));
            string htmlContent;
            using (var wr = req.GetResponse())
            {
                var response = wr.GetResponseStream();
                Debug.Assert(response != null, "htmlresponse != null");
                var sr = new StreamReader(response, Encoding.UTF8);
                htmlContent = sr.ReadToEnd();
            }
            return htmlContent;
        }

        public static string GetHtml2(Uri uri)
        {
            MyWebClient mwc = new MyWebClient();
            return mwc.DownloadString(uri);

        }

        public static string GetImageExtension(string imageUrl)
        {
            var ext = Path.GetExtension(imageUrl);
            if (ext == null || ext.Length > 5)
                return String.Empty;
            return Path.GetExtension(imageUrl);
        }

        public static string DownloadImage(string url, string filePath, params string[] referURL)
        {
            var wc = new MyWebClient();
            if (referURL.Length > 0)
            {
                wc.ReferURL = referURL[0];
            }
            wc.DownloadFile(url, filePath);
            return filePath;
        }

        public static List<string> GetAllImagesFromHtml(string rawHtml)
        {
            List<string> list = new List<string>();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(rawHtml);

            var nodes = doc.DocumentNode.SelectNodes("//img");
            if (nodes == null)
            {
                return list;
            }

            foreach (var item in nodes)
            {
                list.Add(item.Attributes["src"].Value);
            }
            return list;
        }

        public static string GetValidFileName(string rawFileName)
        {
            var fileName = new StringBuilder(rawFileName);
            var invalidFileName = new[] { '?', '\\', '*', '"', '<', '>', '|', '/', '.', ':', '+' };
            foreach (var chr in invalidFileName)
            {
                fileName.Replace(chr, '_');
            }
            return fileName.ToString();
        }

        public static string CreateTableOfContent(IEnumerable<Article> articles)
        {
            var toc = new XElement("html",
                                new XElement("head",
                                        new XElement("Title", "Table Of Contents")
                                ),
                                new XElement("body",
                                     new XElement("div",
                                        new XElement("h1",
                                            new XElement("b", "TABLE OF CONTENTS")
                                ))));
            var body = toc.Element("body");
            Debug.Assert(body != null, "body != null");
            var div = body.Element("div");
            var articlesInGroup = from a in articles
                                  group a by a.Cat into g
                                  orderby g.Key
                                  select g;

            foreach (var c in articlesInGroup)
            {
                var category = new XElement("b", c.Key);
                var ul = new XElement("ul");
                foreach (var article in c)
                {
                    ul.Add(new XElement("li",
                                new XElement("a", new XAttribute("href", article.FileName), article.Title)));

                }
                Debug.Assert(div != null, "div != null");
                div.Add(category, ul);
            }
            return toc.ToString();
        }

        public static string CreateOpf(IEnumerable<Article> articles, string title)
        {
            XNamespace dc = "http://purl.org/dc/elements/1.1/";
            var odf = new XElement("package", new XAttribute(XNamespace.Xmlns + "dc", "http://purl.org/dc/elements/1.1/"),
                                   new XElement("metadata"),
                                   new XElement("manifest"),
                                   new XElement("spine"),
                                   new XElement("guide"));
            var metadata = odf.Element("metadata");
            Debug.Assert(metadata != null, "metadata != null");
            metadata.Add(new XElement(dc + "title", title));
            metadata.Add(new XElement(dc + "language", "en-us"));
            metadata.Add(new XElement(dc + "date", DateTime.Now.ToShortDateString()));
            var manifeast = odf.Element("manifest");
            Debug.Assert(manifeast != null, "manifeast != null");
            manifeast.Add(new XElement("item", new XAttribute("id", "item1"), new XAttribute("media-type", "application/xhtml+xml"), new XAttribute("href", "toc.html")));

            var spine = odf.Element("spine");
            Debug.Assert(spine != null, "spine != null");
            spine.Add(new XElement("itemref", new XAttribute("idref", "item1")));
            var guide = odf.Element("guide");
            Debug.Assert(guide != null, "guide != null");
            guide.Add(new XElement("reference", new XAttribute("type", "toc"), new XAttribute("title", "able of Contents"), new XAttribute("href", "toc.html")
                                     ));
            var num = 2;
            foreach (var article in articles.OrderBy(a => a.Cat))
            {
                manifeast.Add(new XElement("item", new XAttribute("id", String.Format("item{0}", num)), new XAttribute("media-type", "application/xhtml+xml"), new XAttribute("href", article.FileName)));
                spine.Add(new XElement("itemref", new XAttribute("idref", String.Format("item{0}", num))));
                num++;
            }
            return odf.ToString();
        }
    }
}
