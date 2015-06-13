using System;
using System.Drawing;
using System.Windows.Forms;
using Library.Entity;
using Library.Processor;

namespace Client
{
    public partial class MainForm : Form
    {
        private readonly Core _core = new
            Core(AppDomain.CurrentDomain.BaseDirectory + "/Settings.xml", AppDomain.CurrentDomain.BaseDirectory + "/LogInfo");

        public MainForm()
        {
            InitializeComponent();
            notifyIcon.Visible = false;
            var mnuItms = new MenuItem[3];
            mnuItms[0] = new MenuItem { Text = @"查看日志", DefaultItem = true };
            mnuItms[0].Click += item_Click;
            mnuItms[1] = new MenuItem("-");
            mnuItms[2] = new MenuItem { Text = @"退出系统" };
            mnuItms[2].Click += item2_Click;
            var notifyiconMnu = new ContextMenu(mnuItms);
            notifyIcon.ContextMenu = notifyiconMnu;
            Pre();
        }

        private void Pre()
        {
            var img = _core.PreLogin();
            if (img != null)
            {
                //需要验证码
                var memStream = new System.IO.MemoryStream(img);
                //定义并实例化Bitmap对象
                var bm = new Bitmap(memStream);
                bm.Save(AppDomain.CurrentDomain.BaseDirectory + "/yzm.bmp");
                imgYzm.ImageLocation = @"yzm.bmp";
            }
            else
            {
                ShowTip(_core.Login(null));
            }
            Visible = false;
        }

        private void submit_Click(object sender, EventArgs e)
        {
            ShowTip(_core.Login(boxYzm.Text.Trim()));
        }

        private void ShowTip(string content)
        {
            notifyIcon.Visible = true;
            ShowInTaskbar = false;
            WindowState = FormWindowState.Minimized;
            notifyIcon.ShowBalloonTip(3000, "Neko", content, ToolTipIcon.Info);

        }

        void item_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(RunTime.LogRootPath);
        }

        void item2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
