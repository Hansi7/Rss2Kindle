using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RSS2KINDLE
{
    public class SinaBlogGetor:BaseGetor,IArticleGetable
    {
        public Article GetArticle(Uri uri , bool IncludeImage, string basePath)
        {
            string htm = Utility.GetHtml(uri);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htm);
            var nodes=  doc.DocumentNode.SelectNodes("//*[@id='sina_keyword_ad_area2']");
            var title =doc.DocumentNode.SelectSingleNode("//*[@id='articlebody']/div[1]/h2");
            string _title = title.InnerText;
            if (nodes!=null)
            {
                HtmlAgilityPack.HtmlDocument result = new HtmlAgilityPack.HtmlDocument();
                result.LoadHtml(nodes[0].OuterHtml);

                var nodesSpan = result.DocumentNode.SelectNodes("//span[@class]");
                if (nodesSpan!=null)
                {
                    foreach (var item in nodesSpan)
                    {
                        item.Remove();
                    }
                }
                
                HtmlAgilityPack.HtmlDocument d2 = new HtmlAgilityPack.HtmlDocument();
                d2.LoadHtml(result.DocumentNode.OuterHtml);
                d2.DocumentNode.rmsssScript().rmStyle();
                try
                {
                    d2.DocumentNode.SelectSingleNode("//ins").Remove();
                }
                catch (Exception)
                {
                    
                }
                
                rmHerfAtt(d2.DocumentNode);
                rmClassAtt(d2.DocumentNode);
                rmAllAttFromDiv(d2.DocumentNode);
                string html5 = "<!DOCTYPE><html><head><title>"+_title+"</title></head><body><h1>"+_title+"</h1>" + d2.DocumentNode.OuterHtml + "</body></html>";
                Article art = new Article();
                art.Title = _title;
                if (IncludeImage)
                {
                    art.FileName = SaveAsHtml(_title, SaveImage(_title, html5, basePath), basePath);
                }
                else
                {
                    html5 = rmImgNode(html5);
                    art.FileName = SaveAsHtml(_title,html5, basePath);
                }
                return art;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 保存网页图片
        /// </summary>
        /// <param name="title">文章的标题、图片保存的子目录</param>
        /// <param name="oHtml">原始的HTML，用来提取IMG标签</param>
        /// <param name="basePath">输出目录</param>
        /// <returns>新的HTML</returns>
        public override string SaveImage(string title,string oHtml,string basePath)
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
                    string image = item.Attributes["real_src"].Value;
                    item.Attributes["real_src"].Remove();
                    string ext = ".jpg";
                    if (Path.GetExtension(image)!="")
                    {
                        ext = Path.GetExtension(image);
                    } 
                    string dir = GetValidFileName(title);
                    dir = Path.Combine(basePath, dir);

                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    Utility.DownloadImage(image, dir + "\\" + ++i + ext);
                    item.Attributes["src"].Value = dir + "\\" + i + ext;
                }
                catch (Exception err)
                {
                    continue;
                }
            }

            return doc.DocumentNode.OuterHtml.ToString();

        }
        /// <summary>
        /// 保存HTML
        /// </summary>
        /// <param name="title">文章的标题、保存的文件名</param>
        /// <param name="oHtml">原始的HTML</param>
        /// <param name="basePath">输出目录</param>
        /// <returns>文件名</returns>
        public override string SaveAsHtml(string title, string oHtml, string basePath)
        {
            return base.SaveAsHtml(title, oHtml, basePath);
        }
    }
}
