namespace PlugInSample_AutoBSD
{
    partial class UserControl1
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

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl1));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.tsbtnStart = new System.Windows.Forms.ToolStripButton();
            this.tSBStop = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExit = new System.Windows.Forms.ToolStripButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvLotSN2 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.DGVIPSN = new System.Windows.Forms.DataGridView();
            this.DGVLotSN1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLotSN2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVIPSN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVLotSN1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.BackColor = System.Drawing.Color.Gray;
            this.toolStripMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStripMain.BackgroundImage")));
            this.toolStripMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(35, 32);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnStart,
            this.tSBStop,
            this.tsbtnExit});
            this.toolStripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripMain.Size = new System.Drawing.Size(40, 631);
            this.toolStripMain.TabIndex = 4;
            this.toolStripMain.Text = "toolStrip2";
            // 
            // tsbtnStart
            // 
            this.tsbtnStart.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnStart.Image")));
            this.tsbtnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnStart.Name = "tsbtnStart";
            this.tsbtnStart.Size = new System.Drawing.Size(37, 36);
            this.tsbtnStart.ToolTipText = "开始";
            this.tsbtnStart.Click += new System.EventHandler(this.tsbtnStart_Click);
            // 
            // tSBStop
            // 
            this.tSBStop.CheckOnClick = true;
            this.tSBStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSBStop.Image = ((System.Drawing.Image)(resources.GetObject("tSBStop.Image")));
            this.tSBStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBStop.Name = "tSBStop";
            this.tSBStop.Size = new System.Drawing.Size(37, 36);
            this.tSBStop.Text = "暂停";
            this.tSBStop.Click += new System.EventHandler(this.tSBStop_Click);
            // 
            // tsbtnExit
            // 
            this.tsbtnExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExit.Image")));
            this.tsbtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExit.Name = "tsbtnExit";
            this.tsbtnExit.Size = new System.Drawing.Size(37, 36);
            this.tsbtnExit.Text = "toolStripButton1";
            this.tsbtnExit.ToolTipText = "Exit";
            this.tsbtnExit.Click += new System.EventHandler(this.tsbtnExit_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox1.ForeColor = System.Drawing.Color.Yellow;
            this.richTextBox1.Location = new System.Drawing.Point(40, 505);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(966, 126);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(40, 502);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(966, 3);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(40, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(966, 502);
            this.panel1.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(966, 502);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.dgvLotSN2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.DGVIPSN);
            this.tabPage1.Controls.Add(this.DGVLotSN1);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(958, 476);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "自动打印表示单";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(496, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 64);
            this.label5.TabIndex = 4;
            this.label5.Text = "label5";
            this.label5.Visible = false;
            // 
            // dgvLotSN2
            // 
            this.dgvLotSN2.AllowUserToAddRows = false;
            this.dgvLotSN2.AllowUserToDeleteRows = false;
            this.dgvLotSN2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLotSN2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            this.dgvLotSN2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvLotSN2.BackgroundColor = System.Drawing.Color.White;
            this.dgvLotSN2.Location = new System.Drawing.Point(17, 114);
            this.dgvLotSN2.MultiSelect = false;
            this.dgvLotSN2.Name = "dgvLotSN2";
            this.dgvLotSN2.ReadOnly = true;
            this.dgvLotSN2.RowHeadersVisible = false;
            this.dgvLotSN2.RowTemplate.Height = 23;
            this.dgvLotSN2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLotSN2.ShowCellToolTips = false;
            this.dgvLotSN2.ShowEditingIcon = false;
            this.dgvLotSN2.Size = new System.Drawing.Size(924, 63);
            this.dgvLotSN2.TabIndex = 3;
            this.dgvLotSN2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVLotSN1_CellClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(574, 201);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 53);
            this.button1.TabIndex = 2;
            this.button1.Text = "重新打印";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DGVIPSN
            // 
            this.DGVIPSN.AllowUserToAddRows = false;
            this.DGVIPSN.AllowUserToDeleteRows = false;
            this.DGVIPSN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGVIPSN.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGVIPSN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVIPSN.Location = new System.Drawing.Point(17, 201);
            this.DGVIPSN.MultiSelect = false;
            this.DGVIPSN.Name = "DGVIPSN";
            this.DGVIPSN.RowHeadersVisible = false;
            this.DGVIPSN.RowTemplate.Height = 23;
            this.DGVIPSN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVIPSN.ShowCellToolTips = false;
            this.DGVIPSN.ShowEditingIcon = false;
            this.DGVIPSN.ShowRowErrors = false;
            this.DGVIPSN.Size = new System.Drawing.Size(410, 220);
            this.DGVIPSN.TabIndex = 0;
            // 
            // DGVLotSN1
            // 
            this.DGVLotSN1.AllowUserToAddRows = false;
            this.DGVLotSN1.AllowUserToDeleteRows = false;
            this.DGVLotSN1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVLotSN1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            this.DGVLotSN1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.DGVLotSN1.BackgroundColor = System.Drawing.Color.White;
            this.DGVLotSN1.Location = new System.Drawing.Point(17, 27);
            this.DGVLotSN1.MultiSelect = false;
            this.DGVLotSN1.Name = "DGVLotSN1";
            this.DGVLotSN1.ReadOnly = true;
            this.DGVLotSN1.RowHeadersVisible = false;
            this.DGVLotSN1.RowTemplate.Height = 23;
            this.DGVLotSN1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVLotSN1.ShowCellToolTips = false;
            this.DGVLotSN1.ShowEditingIcon = false;
            this.DGVLotSN1.Size = new System.Drawing.Size(924, 63);
            this.DGVLotSN1.TabIndex = 0;
            this.DGVLotSN1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVLotSN1_CellClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "已打印标识单：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "标识单列表";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "随工单2信息";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "随工单1";
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.toolStripMain);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(1006, 631);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLotSN2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVIPSN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVLotSN1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton tsbtnStart;
        private System.Windows.Forms.ToolStripButton tsbtnExit;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton tSBStop;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvLotSN2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView DGVIPSN;
        private System.Windows.Forms.DataGridView DGVLotSN1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
    }
}
