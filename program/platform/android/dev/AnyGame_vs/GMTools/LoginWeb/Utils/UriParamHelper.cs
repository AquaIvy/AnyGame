using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Adventure.Server.Logic.Logic.LoginPlatform
{
    /// <summary>
    /// URL解析工具
    /// </summary>
    class UriParamHelper
    {

        private List<Tuple<string, string>> param = new List<Tuple<string, string>>();

        public UriParamHelper(string uriParam)
        {
            foreach(var p in uriParam.Split("&".ToArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                var s2 = p.Split('=');
                if (s2.Length > 1)
                    param.Add(new Tuple<string, string>(s2[0], s2[1]));
            }
        }

        public string GetString(string key)
        {
            var item = param.FirstOrDefault(o => o.Item1 == key);
            if (item == null)
                return string.Empty;

            return item.Item2;
        }

        public int GetInt(string key)
        {
            var item = param.FirstOrDefault(o => o.Item1 == key);
            if (item == null)
                return int.MinValue;

            int ret;
            if (int.TryParse(item.Item2, out ret))
                return ret;
            return int.MinValue;
        }
    }

    static class Md5Extend
    {
        /// <summary>
        /// 获得md5值
        /// </summary>
        /// <param name="md5base"></param>
        /// <returns></returns>
        public static string GetMd5(this string md5base)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(md5base, "MD5");
        }
    }
}
