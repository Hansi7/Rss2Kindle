namespace RSS2KINDLE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button4 = new System.Windows.Forms.Button();
            this.txt_info = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.txt_to = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_SendToKindle = new System.Windows.Forms.CheckBox();
            this.btn_EditConfig = new System.Windows.Forms.Button();
            this.cb_IncludePic = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_SaveMail = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.LimeGreen;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(16, 60);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(300, 67);
            this.button4.TabIndex = 3;
            this.button4.Text = "Go!";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txt_info
            // 
            this.txt_info.Location = new System.Drawing.Point(12, 142);
            this.txt_info.Multiline = true;
            this.txt_info.Name = "txt_info";
            this.txt_info.Size = new System.Drawing.Size(794, 275);
            this.txt_info.TabIndex = 4;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(714, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(89, 12);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "作者:@麦田呱呱";
            this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // txt_to
            // 
            this.txt_to.Location = new System.Drawing.Point(134, 18);
            this.txt_to.Name = "txt_to";
            this.txt_to.Size = new System.Drawing.Size(148, 21);
            this.txt_to.TabIndex = 0;
            this.txt_to.Leave += new System.EventHandler(this.txt_to_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_SaveMail);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_to);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(472, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 83);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "发送到Kindle设置";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "请把Rss2Kindle@163.com加入到已认可的发件人列表中";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "你的Kindle邮箱地址";
            // 
            // cb_SendToKindle
            // 
            this.cb_SendToKindle.AutoSize = true;
            this.cb_SendToKindle.Checked = true;
            this.cb_SendToKindle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_SendToKindle.Location = new System.Drawing.Point(472, 22);
            this.cb_SendToKindle.Name = "cb_SendToKindle";
            this.cb_SendToKindle.Size = new System.Drawing.Size(96, 16);
            this.cb_SendToKindle.TabIndex = 12;
            this.cb_SendToKindle.Text = "发送到Kindle";
            this.cb_SendToKindle.UseVisualStyleBackColor = true;
            this.cb_SendToKindle.CheckStateChanged += new System.EventHandler(this.cb_SendToKindle_CheckStateChanged);
            // 
            // btn_EditConfig
            // 
            this.btn_EditConfig.Location = new System.Drawing.Point(270, 4);
            this.btn_EditConfig.Name = "btn_EditConfig";
            this.btn_EditConfig.Size = new System.Drawing.Size(113, 23);
            this.btn_EditConfig.TabIndex = 13;
            this.btn_EditConfig.Text = "直接编辑配置文件";
            this.btn_EditConfig.UseVisualStyleBackColor = true;
            this.btn_EditConfig.Click += new System.EventHandler(this.btn_EditConfig_Click);
            // 
            // cb_IncludePic
            // 
            this.cb_IncludePic.AutoSize = true;
            this.cb_IncludePic.Checked = true;
            this.cb_IncludePic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_IncludePic.Location = new System.Drawing.Point(192, 8);
            this.cb_IncludePic.Name = "cb_IncludePic";
            this.cb_IncludePic.Size = new System.Drawing.Size(72, 16);
            this.cb_IncludePic.TabIndex = 14;
            this.cb_IncludePic.Text = "包含图片";
            this.cb_IncludePic.UseVisualStyleBackColor = true;
            this.cb_IncludePic.Click += new System.EventHandler(this.cb_IncludePic_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(121, 7);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(43, 21);
            this.numericUpDown1.TabIndex = 15;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Click += new System.EventHandler(this.numericUpDown1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "每个源读取文章数";
            // 
            // btn_SaveMail
            // 
            this.btn_SaveMail.Location = new System.Drawing.Point(300, 16);
            this.btn_SaveMail.Name = "btn_SaveMail";
            this.btn_SaveMail.Size = new System.Drawing.Size(25, 23);
            this.btn_SaveMail.TabIndex = 17;
            this.btn_SaveMail.Text = "存";
            this.btn_SaveMail.UseVisualStyleBackColor = true;
            this.btn_SaveMail.Click += new System.EventHandler(this.btn_SaveMail_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 429);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.cb_IncludePic);
            this.Controls.Add(this.btn_EditConfig);
            this.Controls.Add(this.cb_SendToKindle);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.txt_info);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "RssToKindle";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txt_info;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox txt_to;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cb_SendToKindle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_EditConfig;
        private System.Windows.Forms.CheckBox cb_IncludePic;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_SaveMail;
    }
}

