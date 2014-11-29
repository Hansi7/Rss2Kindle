using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSS2KINDLE
{
    interface IArticleGetable
    {
        Article GetArticle(Uri uri,bool IncludeImage,string basePath);
        string SaveAsHtml(string title, string oHtml, string basePath);
        string SaveImage(string title, string oHtml, string basePath);
    }
}
