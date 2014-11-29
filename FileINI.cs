using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace RSS2KINDLE
{
    public class FileINI
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        //申明INI文件的读操作函数GetPrivateProfileString()
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public FileINI(string filename)
        {
            try
            {
                this.File = new System.IO.FileInfo(filename);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private System.IO.FileInfo File { get; set; }

        public bool WriteINI(string section,string key,string value)
        {
            if (File!=null)
            {
                long r = WritePrivateProfileString(section, key, value, File.FullName);
                if (r!=0)
                {
                    return true;
                }
            }
            return false;
        }
        public string ReadINI(string section, string key)
        {
            StringBuilder sb = new StringBuilder();
            if (File!=null)
            {
                GetPrivateProfileString(section, key, string.Empty, sb, int.MaxValue, File.FullName);
            }
            return sb.ToString();
        }
    }
}
