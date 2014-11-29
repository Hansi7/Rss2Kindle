using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Diagnostics;
using System.IO;

namespace PicturesToMobi
{
    class PageGen
    {
        public static string gen(FileInfo f)
        {
//            <html>
//  <head>
//    <Title>Table Of Contents</Title>
//  </head>
//  <body>
//    <div>
//      <h1>
//        <b>目录</b>
//      </h1>
//    </div>
//  </body>
//</html>


            var toc = new XElement("html",
                      new XElement("head",
                              new XElement("Title", f.Name)
                      ),
                      new XElement("body",
                           new XElement("div",
                            new XElement("img", new XAttribute("src", f.Name))
                      )));


            return toc.ToString();
        }
    }
}
