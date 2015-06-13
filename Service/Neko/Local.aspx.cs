using System;
using System.Drawing;
using System.Web;

namespace Service.Neko
{
    public partial class Local : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var img = Global.Core.PreLogin();
                if (img != null)
                {
                    //需要验证码
                    var memStream = new System.IO.MemoryStream(img);
                    //定义并实例化Bitmap对象
                    var bm = new Bitmap(memStream);
                    bm.Save(HttpContext.Current.Server.MapPath("/Neko/yzm.bmp"));
                    imgYzm.ImageUrl = "/Neko/yzm.bmp";
                }
                else
                {
                    Response.Write("<script>alert('" + Global.Core.Login(null) + "');</script>");
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('" + Global.Core.Login(boxYzm.Text.Trim()) + "');</script>");
        }
    }
}