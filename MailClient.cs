using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace RSS2KINDLE
{
    class MailClient
    {
        private string _smtpServer;
        public string SmtpServer
        {
            get
            { 
                return "smtp." + _server; 
            }
            set { _smtpServer = value; }
        }
        private string _server;
        private string _username;
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                this._username = value;
            }
        }
        private string _emailFrom;
        private string _emailTo;
        public MainForm UI { get; set; }
        public MailClient(string emailFrom,string emailto)
        {
            this._emailFrom = emailFrom;
            this._emailTo = emailto;
            if (!Regex.IsMatch(emailFrom,@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                throw new Exception("发送邮箱格式不正确！");   
            }
            if (!Regex.IsMatch(emailto, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                throw new Exception("kindle邮箱格式不正确！");
            }
            SmtpClient sc = new SmtpClient();
            Regex rex = new Regex(@"@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            Regex rexUser = new Regex(@"\w+([-+.]\w+)*", RegexOptions.IgnoreCase);
            try
            {
                _server = rex.Match(emailFrom).Value.Substring(1);

                _username = rexUser.Match(emailFrom).Value;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void SendMail(string fileName,string pwd)
        {
            try
            {
                System.Net.Mail.Attachment att = new Attachment(fileName);
                System.Net.Mail.MailMessage msg = new MailMessage(_emailFrom, _emailTo, "convert", "");
                msg.Attachments.Add(att);
                System.Net.Mail.SmtpClient sc = new SmtpClient(SmtpServer, 25);
                sc.Credentials = new System.Net.NetworkCredential(_username, pwd);
                sc.SendCompleted += new SendCompletedEventHandler(sc_SendCompleted);
                sc.SendAsync(msg, null);
                UI.Popmessage("正在发送邮件,上传附件中,请勿关闭程序...");
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        void sc_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            UI.Popmessage("发送成功！");
        }

	
    }
}
