using System;
using System.Collections.Generic;
using System.Text;

namespace KVP_Config
{
    public class ConfigResult
    {
        private Dictionary<string, Dictionary<string, string>> res = new Dictionary<string, Dictionary<string, string>>();

        public void Add(string name, Dictionary<string, string> dic)
        {
            this.res.Add(name, dic);
        }

        public Dictionary<string,string> this[string index]
        {
            get 
            {
                try
                {
                    return this.res[index];
                }
                catch (KeyNotFoundException err)
                {
                    throw new KeyNotFoundException("没有找到指定的配置信息 " + err.Message);
                }
            }
        }

    }
}
