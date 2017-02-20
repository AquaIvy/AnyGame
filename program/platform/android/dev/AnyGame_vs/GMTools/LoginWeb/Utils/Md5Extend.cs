using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace LoginWeb.Utils
{
    public static class Md5Extend
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