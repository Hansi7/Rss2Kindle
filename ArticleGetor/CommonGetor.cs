using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSS2KINDLE
{
    public class CommonGetor:BaseGetor,IArticleGetable
    {
        public Article GetArticle(Uri uri,bool IncludeImage, string basePath)
        {
            NReadability.NReadabilityWebTranscoder transcoder = new NReadability.NReadabilityWebTranscoder();
            var domparam = new NReadability.DomSerializationParams();
            domparam.DontIncludeContentTypeMetaElement = true;
            domparam.DontIncludeDocTypeMetaElement = true;
            domparam.DontIncludeGeneratorMetaElement = true;
            domparam.DontIncludeMobileSpecificMetaElements = true;
            var input = new NReadability.WebTranscodingInput(uri.OriginalString);
            input.DomSerializationParams = domparam;
            var output = transcoder.Transcode(input);
            var title = output.ExtractedTitle;
            var content = output.ExtractedContent;

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(content);
            rmHerfAtt(doc.DocumentNode);
            rmClassAtt(doc.DocumentNode);
            rmAllAttFromDiv(doc.DocumentNode);
            content = doc.DocumentNode.OuterHtml;
            Article art = new Article();
            art.Title = title;

            string ORhtml = rmStyle(content);



            if (IncludeImage)
            {
                string html = SaveImage(title, ORhtml, basePath);
                art.FileName = SaveAsHtml(title, html, basePath);
            }
            else
            {
                ORhtml= rmImgNode(ORhtml);
                art.FileName = SaveAsHtml(title, ORhtml, basePath);
            }
            return art;
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
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="title">名称</param>
        /// <param name="oHtml">html</param>
        /// <param name="basePath">输出</param>
        /// <returns>新的HTML</returns>
        public override string SaveImage(string title, string oHtml, string basePath)
        {
            return base.SaveImage(title, oHtml, basePath);
        }
    }
}
