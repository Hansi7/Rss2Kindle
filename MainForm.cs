using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using KVP_Config;
using System.Text.RegularExpressions;

namespace RSS2KINDLE
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }
        private const string KindleGenApp = @"tools\kindlegen.exe ";


        private void GenINI()
        {
            ini.WriteINI("Feeds", "果壳", "http://www.guokr.com/rss/");
            ini.WriteINI("Feeds", "韩寒", "http://blog.sina.com.cn/rss/1191258123.xml");
            ini.WriteINI("Feeds", "文化焦点", "http://rss.sina.com.cn/edu/focus19.xml");
            ini.WriteINI("Settings", "SendToKindle", "1");
            ini.WriteINI("Settings", "Mail", "rss2kindle@163.com");
            ini.WriteINI("Settings", "Image", "1");
            ini.WriteINI("Settings", "MaxCount", "10");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            ini = new FileINI("Config.ini");
            if (!File.Exists("Config.ini"))
            {
                GenINI();
            }
            r = new KVP_Config.ConfigsReader("config.ini", Encoding.Default);

            config = r.GetResult();
            txt_to.Text = ini.ReadINI("Settings", "Mail");
            numericUpDown1.Value = int.Parse(ini.ReadINI("Settings", "MaxCount").ToString());
            cb_IncludePic.Checked = ini.ReadINI("Settings", "Image") == "1" ? true : false;
            txt_info.Text = @"这个东西实在是太简陋了。

它只能完成一件事情：把RSS地址中源的文章下载下来，去除广告。制作成mobi文件。

需要.Net 4.0。windowsXP有点难，Win7 Win8都应该没有问题了。

修改config.ini里面的rss地址。启动软件再点击Start就可以了。

BUG一定是超级多的，发现了请报告给我，谢谢。

新浪微博 @麦田呱呱  2014年3月22日";

        }



        ConfigResult config;
        ConfigsReader r;

        FileINI ini;
        int maxCount;

        private void button4_Click(object sender, EventArgs e)
        {
            ini.WriteINI("Settings", "MaxCount", this.numericUpDown1.Value.ToString());
            this.button4.Enabled = false;
            this.button4.BackColor = Color.Yellow;
            this.button4.Text = "Wait...";

            if (!int.TryParse(ini.ReadINI("Settings", "MaxCount"), out maxCount))
            {
                ini.WriteINI("Settings", "MaxCount", "10");
            }
            
            config = r.GetResult();
            txt_info.Text = "正在准备资源...";
            Thread th = new Thread(Genarator);
            th.IsBackground = true;
            th.Start();
        }
        private bool CheckValidSendParam()
        {
            if (cb_SendToKindle.Checked)
            {
                if (Regex.IsMatch(txt_to.Text, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
            
        }
        private void Genarator()
        {
            if (CheckValidSendParam() == false)
            {
                MessageBox.Show("发送至Kindle参数不完整！请\"填写完整kindle邮箱\" 或者 \"去掉发送选项\"，使用USB数据线传送mobi文件。");
                return;
            }
            string outputpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OutPut_" + DateTime.Now.ToString("MMdd_HHmm"));
            if (!Directory.Exists(outputpath))
            {
                Directory.CreateDirectory(outputpath);
            }
            List<Article> articles = new List<Article>();
            if (config["Feeds"].Count > 0)
            {
                Dictionary<string, string> dicfeeds = config["Feeds"];
                foreach (var item in dicfeeds)
                {
                    RssProcess rssss = new RssProcess(item.Value, item.Key, outputpath);
                    rssss.UI = this;
                    try
                    {
                        Popmessage(item.Key);
                        var arts = rssss.Process((ini.ReadINI("Settings", "Image") == "1"), maxCount);
                        articles.AddRange(arts);
                    }
                    catch (QDFeedParser.InvalidFeedXmlException err)
                    {
                        Popmessage("无法读取Feed源:" + item.Key);
                    }
                    catch (QDFeedParser.MissingFeedException err)
                    {
                        Popmessage("无法读取Feed源:" + item.Key);
                    }
                }
            }



            var toc = Utility.CreateTableOfContent(articles);

            File.WriteAllText(outputpath + "\\toc.html", toc, Encoding.UTF8);

            var opf = Utility.CreateOpf(articles, DateTime.Now.ToString("MM/dd HH:mm") + " Created By RssToKindle");
            string opfFilename = "RssToKindle" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".opf";
            File.WriteAllText(outputpath + "\\" + opfFilename, opf);
            string mobifile = ConvertToMobi(outputpath + "\\" + opfFilename, "");
            Popmessage("mobi文件已经生成,请及时清理不需要的文件。");
            Popmessage(".......................................");
            Popmessage("准备发送邮件");
            SendEmail(mobifile);
            this.button4.Enabled = true;
            this.button4.BackColor = Color.LimeGreen;
            this.button4.Text = "Go!";
        }

        private void SendEmail(string mobifile)
        {
            if (ini.ReadINI("Settings", "SendToKindle") == "0")
            {
                Popmessage("用户选择不需要发送邮件");
                Popmessage("All Done！");
                return;
            }
            try
            {
                _mailClient = new MailClient("rss2kindle@163.com", txt_to.Text);
                _mailClient.UI = this;
                _mailClient.SmtpServer = "smtp.163.com";
                _mailClient.UserName = "RSS2KINDLE";
                _mailClient.SendMail(mobifile,"cptbtptpbcptdtpt");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private string ConvertToMobi(string sourFilePath, string outputFilePath)
        {
            //var currentFolder = Directory.GetCurrentDirectory();
            //var convertor = Path.Combine(currentFolder, KindleGenApp);
            var stratInfo = new ProcessStartInfo { FileName = KindleGenApp, Arguments = string.Format("\"{0}\"", sourFilePath) };
            var converProcess = Process.Start(stratInfo);
            converProcess.WaitForExit();
            //RunAndWaitForExit(stratInfo, 10000);

            Process.Start("explorer.exe", "/select," + sourFilePath);

            var a = Path.GetDirectoryName(sourFilePath);
            var b = Path.GetFileNameWithoutExtension(sourFilePath) + ".mobi";
            return Path.Combine(a, b);

        }

        private static void RunAndWaitForExit(ProcessStartInfo startInfo, int milliSeconds)
        {
            //startInfo.RedirectStandardOutput = true;
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //startInfo.UseShellExecute = false;
            //var convertProcess = Process.Start(startInfo);
            //convertProcess.WaitForExit(milliSeconds);
        }

        internal void Popmessage(string p)
        {
            txt_info.AppendText(p + "\r\n");
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://weibo.com/virush");
        }

        private MailClient _mailClient;

        private void cb_SendToKindle_CheckStateChanged(object sender, EventArgs e)
        {
            if (cb_SendToKindle.Checked)
            {
                ini.WriteINI("Settings", "SendToKindle", "1");
                groupBox1.Enabled = true;
            }
            else
            {
                ini.WriteINI("Settings", "SendToKindle", "0");
                groupBox1.Enabled = false;
            }
            
        }

        private void txt_to_Leave(object sender, EventArgs e)
        {
            ini.WriteINI("Settings", "Mail", txt_to.Text.Trim());
        }

        private void btn_EditConfig_Click(object sender, EventArgs e)
        {
            Process.Start("config.ini");
        }

        private void cb_IncludePic_Click(object sender, EventArgs e)
        {
            string va;
            if (cb_IncludePic.Checked)
	        {
                va = "1";
	        }
            else
        	{
                va = "0";
	        }
            ini.WriteINI("Settings", "Image", va);
        }

        private void btn_SaveMail_Click(object sender, EventArgs e)
        {
            ini.WriteINI("Settings", "Mail", txt_to.Text);
        }

        private void numericUpDown1_Click(object sender, EventArgs e)
        {
            ini.WriteINI("Settings", "MaxCount", this.numericUpDown1.Value.ToString());
        }


    }
}
