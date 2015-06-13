using System;
using System.IO;
using System.Web;

namespace Service.Neko
{
    public partial class Reboot : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            File.SetLastWriteTime(HttpContext.Current.Request.MapPath("~\\Web.config"), DateTime.Now);
            Response.Write("Reboot Success");
        }
    }
}