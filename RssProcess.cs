using NReadability;
using QDFeedParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RSS2KINDLE
{
    public class RssProcess
    {
        string rssURL;
        string cat;
        string outputpath;

        public MainForm UI { get; set; }

        public RssProcess(string url,string cat,string outputpath)
        {
            this.rssURL = url;
            this.cat = cat;
            this.outputpath = outputpath;
        }

        public List<Article> Process(bool IsDownImage,int maxcount)
        {
            HttpFeedFactory factory = new HttpFeedFactory();
            IFeed feed = factory.CreateFeed(new Uri(this.rssURL));
            List<Article> articles = GetArticles(feed,maxcount,IsDownImage);
            Console.WriteLine("GET Artilces!");
            articles.ForEach(ar => ar.Cat = cat);
            return articles;
        }

        #region 不知道用哪个方式好，用transcoder里面的URL直接获取，还是获取了HTML用Transcoder改呢

        private List<Article> GetArticles(IFeed feed,int maxcount,bool isDownImage)
        {
            List<Article> articles = new List<Article>();
            int i = 0;
            foreach (var item in feed.Items)
            {
                if (i >= maxcount)
                {
                    break;
                }
                UI.Popmessage(++i + "/" + feed.Items.Count.ToString() + ":\t" + item.Title);
                try
                {
                    var uri = new Uri(item.Link);
                    Article art;
                    if (uri.Host == "blog.sina.com.cn")
                    {
                        art = new SinaBlogGetor().GetArticle(uri, isDownImage,outputpath);
                    }
                    else
                    {
                        art = new CommonGetor().GetArticle(uri, isDownImage,outputpath);
                    }
                    art.RSSDatePublished = item.DatePublished;
                    art.RSSAuthor = item.Author;
                    articles.Add(art);
                }
                catch (Exception err)
                {
                    UI.Popmessage(err.Message);
                    continue;
                }

            }
            return articles;
        }



        #endregion

    }
}
