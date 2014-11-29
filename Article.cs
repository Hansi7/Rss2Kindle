using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RSS2KINDLE
{
    public class Article
    {
        public string Title { get; set; }
        public DateTime RSSDatePublished { get; set; }
        public string RSSAuthor { get; set; }
        public string Cat { get; set; }
        public string FileName { get; set; }
    }
}
