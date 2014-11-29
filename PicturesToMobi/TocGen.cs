using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Diagnostics;
using System.IO;

namespace PicturesToMobi
{
    class TocGen
    {
        public static string CreateTableOfContent(List<FileInfo> list)
        {
            var toc = new XElement("html",
                                new XElement("head",
                                        new XElement("Title", "Table Of Contents")
                                ),
                                new XElement("body",
                                     new XElement("div",
                                        new XElement("h1",
                                            new XElement("b", "目 录")
                                ))));
            var body = toc.Element("body");
            Debug.Assert(body != null, "body != null");
            var div = body.Element("div");


            var category = new XElement("b", "本书由PicturesToMobi生成   新浪微博：@麦田呱呱");
            var ul = new XElement("ul");

            foreach (var item in list)
            {
                    ul.Add(new XElement("li",
                        new XElement("a", new XAttribute("href", Path.GetFileNameWithoutExtension(item.FullName)+".html"), item.Name)));
            }


            Debug.Assert(div != null, "div != null");
            div.Add(category, ul);

            return toc.ToString();
        }
    }
}
