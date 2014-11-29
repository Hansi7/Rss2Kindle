using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Diagnostics;
using System.IO;

namespace PicturesToMobi
{
    class OpfGen
    {
        public static string gen(List<FileInfo> list,string bookTitle)
        {



            XNamespace dc = "http://purl.org/dc/elements/1.1/";
            var odf = new XElement("package", new XAttribute(XNamespace.Xmlns + "dc", "http://purl.org/dc/elements/1.1/"),
                                   new XElement("metadata"),
                                   new XElement("manifest"),
                                   new XElement("spine"),
                                   new XElement("guide"));
            var metadata = odf.Element("metadata");
            Debug.Assert(metadata != null, "metadata != null");
            metadata.Add(new XElement(dc + "title", bookTitle));
            metadata.Add(new XElement(dc + "language", "zh"));
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
            foreach (var di in list)
            {
                manifeast.Add(new XElement("item",new XAttribute("id",string.Format("item{0}", num)),new XAttribute("media-type", "application/xhtml+xml"),new XAttribute("href",Path.GetFileNameWithoutExtension(di.FullName)+".html")));
                num++;
                manifeast.Add(new XElement("item", new XAttribute("id", String.Format("item{0}", num)), new XAttribute("media-type", "application/image/jpeg"), new XAttribute("href", di.Name)));
                spine.Add(new XElement("itemref", new XAttribute("idref", String.Format("item{0}", num))));
                num++;
                //<item id="item3" media-type="application/xhtml+xml" href="UG-C2.html"></item>
            }
            return odf.ToString();


        }

        //
        //<package xmlns="http://www.idpf.org/2007/opf" 
        //            xmlns:dc="http://purl.org/dc/elements/1.1/" 
        //            unique-identifier="bookid" version="2.0">
        //  <metadata>
        //    <dc:title>Hello World: My First EPUB</dc:title>
        //    <dc:creator>My Name</dc:creator>
        //    <dc:identifier id="bookid">urn:uuid:12345</dc:identifier>
        //    <meta name="cover" content="cover-image" />
        //  </metadata>
        //  <manifest>
        //    <item id="ncx" href="toc.ncx" media-type="text/xml"/>
        //    <item id="cover" href="title.html" media-type="application/xhtml+xml"/>
        //    <item id="content" href="content.html" media-type="application/xhtml+xml"/>
        //    <item id="cover-image" href="images/cover.png" media-type="image/png"/>
        //    <item id="css" href="stylesheet.css" media-type="text/css"/>
        //  </manifest>
        //  <spine toc="ncx">
        //    <itemref idref="cover" linear="no"/>
        //    <itemref idref="content"/>
        //  </spine>
        //  <guide>
        //    <reference href="cover.html" type="cover" title="Cover"/>
        //  </guide>
        //</package>
    }
}
