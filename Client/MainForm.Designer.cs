namespace Client
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.submit = new System.Windows.Forms.Button();
            this.boxYzm = new System.Windows.Forms.TextBox();
            this.imgYzm = new System.Windows.Forms.PictureBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imgYzm)).BeginInit();
            this.SuspendLayout();
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(29, 158);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(142, 49);
            this.submit.TabIndex = 0;
            this.submit.Text = "登录";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // boxYzm
            // 
            this.boxYzm.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.boxYzm.Location = new System.Drawing.Point(21, 113);
            this.boxYzm.Name = "boxYzm";
            this.boxYzm.Size = new System.Drawing.Size(159, 21);
            this.boxYzm.TabIndex = 1;
            // 
            // imgYzm
            // 
            this.imgYzm.Location = new System.Drawing.Point(21, 24);
            this.imgYzm.Name = "imgYzm";
            this.imgYzm.Size = new System.Drawing.Size(159, 65);
            this.imgYzm.TabIndex = 2;
            this.imgYzm.TabStop = false;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Neko";
            this.notifyIcon.Visible = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(205, 228);
            this.Controls.Add(this.imgYzm);
            this.Controls.Add(this.boxYzm);
            this.Controls.Add(this.submit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Neko";
            ((System.ComponentModel.ISupportInitialize)(this.imgYzm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.TextBox boxYzm;
        private System.Windows.Forms.PictureBox imgYzm;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

