using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace KVP_Config
{
    /// <summary>
    /// 配置读取器
    /// </summary>
    public class ConfigsReader
    {
        string _path = string.Empty;
        Encoding _code = null;
        /// <summary>
        /// 创建一个配置文件读取器
        /// </summary>
        /// <param name="path">配置文件的路径包含扩展名</param>
        /// <param name="coding">配置文件的编码</param>
        public ConfigsReader(string path,Encoding coding)
        {
            this._path = path;
            this._code = coding;
        }
        #region 读取文本文件
        private List<string> _load()
        {
            if (_path == string.Empty)
            {
                throw new ApplicationException(Properties.Resources.PathEmpty);
            }
            List<string> liststring = new List<string>();
            string[] arr;
            if (File.Exists(this._path))
            {
                try
                {
                    arr = File.ReadAllLines(this._path,_code);
                }
                catch (Exception err)
                {
                    throw err;
                }
               
                foreach (string item in arr)
                {
                    liststring.Add(item);
                }
            }
            else
            {
                throw new ApplicationException(Properties.Resources.PathWrong);
            }
            return liststring;
        }
        #endregion
        #region 分析键值对
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns>KeyValuePairs </returns>
        public ConfigResult GetResult()
        {
            ConfigResult con = new ConfigResult();
            string name = "Defult";

            List<string> data = _load();
            if (data.Count==0)
            {
                return null;
            }

            bool newGroupStart = false;

            Dictionary<string, string> _kvp = new Dictionary<string, string>();

            foreach (string item in data)
            {
                int i = item.IndexOf("=");
                if (i==-1)
                {
                    int s = item.IndexOf("[");
                    int e = item.IndexOf("]");
                    if (s==-1 || e==-1)
                    {
                        continue;
                    }
                    if (newGroupStart == false)//第一次开始不加入结果
                    {
                        newGroupStart = true;
                    }
                    else if (newGroupStart == true)//第二次找到标记加之前的为结果
                    {
                        con.Add(name, _kvp);
                        _kvp = new Dictionary<string, string>();
                    }
                    name = item.Substring(s + 1, e - s -1);
                    continue;
                }
                string key = item.Substring(0, i).Trim();
                string value = item.Substring(i + 1).Trim();
                _kvp.Add(key,value);
            }
            con.Add(name, _kvp);
            return con;
        }
        #endregion

    }
}
