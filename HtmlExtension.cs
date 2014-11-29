using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSS2KINDLE
{
    public static class HtmlExtension
    {
        public static HtmlAgilityPack.HtmlNode rmsssScript(this HtmlAgilityPack.HtmlNode node)
        {
            if (node.Descendants("script") != null)
            {
                foreach (var script in node.Descendants("script").ToArray())
                {
                    script.Remove();
                }
            }
            return node;
        }
        public static HtmlAgilityPack.HtmlNode rmStyle(this HtmlAgilityPack.HtmlNode node)
        {
            if (node.Descendants("style") != null)
            {
                foreach (var script in node.Descendants("style").ToArray())
                {
                    script.Remove();
                }
            }
            return node;
        }
    }
}
