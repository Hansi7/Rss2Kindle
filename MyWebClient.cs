using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace RSS2KINDLE
{
    public class MyWebClient : WebClient
    {
        public CookieContainer m_container = new CookieContainer();

        string refrer = string.Empty;
        public string ReferURL
        {
            get
            {

                return this.refrer;
            }
            set
            {
                this.refrer = value;
            }
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest webRequest = base.GetWebRequest(address);
            if (webRequest is HttpWebRequest)
            {
                (webRequest as HttpWebRequest).CookieContainer = this.m_container;
                if (ReferURL != string.Empty)
                {
                    (webRequest as HttpWebRequest).Referer = ReferURL;
                }
            }
            return webRequest;
        }
    }
}
