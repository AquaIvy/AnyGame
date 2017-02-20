using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginWeb
{
    /// <summary>
    /// 版本验证页面
    /// </summary>
    public partial class VerAuth : System.Web.UI.Page
    {
        //http://localhost:14044/VerAuth.aspx?channel=anygame&phonePlatformTypes=android&cver=4
        protected void Page_Load(object sender, EventArgs e)
        {
            string channel = Request.QueryString["channel"];
            string phoneType = Request.QueryString["phonePlatformTypes"];
            string clientVersion = Request.QueryString["cver"];

            if (channel == null || phoneType == null || clientVersion == null)
            {
                Logs.Error("parameter not complete");
                Response.Write("parameter not complete");
                return;
            }

            //状态 state 0成功 1下载新包 2不可更新
            //描述 describe
            var state = 0;
            var describe = "sucess";
            string ret = string.Format("{0}", describe);
            Response.Write(ret);
            Response.End();
        }
    }
}