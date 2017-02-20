using LoginWeb.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoginWeb
{
    public partial class ClearCacheData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AccountController.ClearDataCache();
            Response.Write("清理完成  " + DateTime.Now);
            Response.End();
        }
    }
}