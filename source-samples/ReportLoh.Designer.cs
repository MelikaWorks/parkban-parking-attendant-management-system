namespace Parkban
{
    partial class ReportLoh
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportLoh));
            this.toolSt = new System.Windows.Forms.ToolStrip();
            this.lbl_Dte = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lbl_Tme = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lbl_Usr = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lbl_Lvl = new System.Windows.Forms.ToolStripLabel();
            this.lbl_LvlT = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.lbl_Net = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tSlbl_Exit = new System.Windows.Forms.ToolStripButton();
            this.stsSt_NDCr = new System.Windows.Forms.StatusStrip();
            this.tlSL_CpyR = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer_Net = new System.Windows.Forms.Timer(this.components);
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolSt.SuspendLayout();
            this.stsSt_NDCr.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolSt
            // 
            this.toolSt.Font = new System.Drawing.Font("B Yekan", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.toolSt.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_Dte,
            this.toolStripSeparator1,
            this.lbl_Tme,
            this.toolStripSeparator2,
            this.lbl_Usr,
            this.toolStripSeparator3,
            this.lbl_Lvl,
            this.lbl_LvlT,
            this.toolStripSeparator5,
            this.lbl_Net,
            this.toolStripSeparator4,
            this.tSlbl_Exit});
            this.toolSt.Location = new System.Drawing.Point(0, 0);
            this.toolSt.Name = "toolSt";
            this.toolSt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolSt.Size = new System.Drawing.Size(784, 25);
            this.toolSt.TabIndex = 3;
            this.toolSt.Text = "toolStrip1";
            // 
            // lbl_Dte
            // 
            this.lbl_Dte.Image = ((System.Drawing.Image)(resources.GetObject("lbl_Dte.Image")));
            this.lbl_Dte.Name = "lbl_Dte";
            this.lbl_Dte.Size = new System.Drawing.Size(50, 22);
            this.lbl_Dte.Text = "Date";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // lbl_Tme
            // 
            this.lbl_Tme.Image = ((System.Drawing.Image)(resources.GetObject("lbl_Tme.Image")));
            this.lbl_Tme.Name = "lbl_Tme";
            this.lbl_Tme.Size = new System.Drawing.Size(52, 22);
            this.lbl_Tme.Text = "Time";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // lbl_Usr
            // 
            this.lbl_Usr.Image = ((System.Drawing.Image)(resources.GetObject("lbl_Usr.Image")));
            this.lbl_Usr.Name = "lbl_Usr";
            this.lbl_Usr.Size = new System.Drawing.Size(50, 22);
            this.lbl_Usr.Text = "User";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // lbl_Lvl
            // 
            this.lbl_Lvl.ForeColor = System.Drawing.Color.Fuchsia;
            this.lbl_Lvl.Name = "lbl_Lvl";
            this.lbl_Lvl.Size = new System.Drawing.Size(15, 22);
            this.lbl_Lvl.Text = "-";
            // 
            // lbl_LvlT
            // 
            this.lbl_LvlT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lbl_LvlT.Name = "lbl_LvlT";
            this.lbl_LvlT.Size = new System.Drawing.Size(15, 22);
            this.lbl_LvlT.Text = "-";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // lbl_Net
            // 
            this.lbl_Net.Image = ((System.Drawing.Image)(resources.GetObject("lbl_Net.Image")));
            this.lbl_Net.Name = "lbl_Net";
            this.lbl_Net.Size = new System.Drawing.Size(43, 22);
            this.lbl_Net.Text = "Net";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tSlbl_Exit
            // 
            this.tSlbl_Exit.Image = ((System.Drawing.Image)(resources.GetObject("tSlbl_Exit.Image")));
            this.tSlbl_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSlbl_Exit.Name = "tSlbl_Exit";
            this.tSlbl_Exit.Size = new System.Drawing.Size(50, 22);
            this.tSlbl_Exit.Text = "خروج";
            // 
            // stsSt_NDCr
            // 
            this.stsSt_NDCr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlSL_CpyR});
            this.stsSt_NDCr.Location = new System.Drawing.Point(0, 540);
            this.stsSt_NDCr.Name = "stsSt_NDCr";
            this.stsSt_NDCr.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.stsSt_NDCr.Size = new System.Drawing.Size(784, 22);
            this.stsSt_NDCr.TabIndex = 4;
            this.stsSt_NDCr.Text = "statusStrip1";
            // 
            // tlSL_CpyR
            // 
            this.tlSL_CpyR.Name = "tlSL_CpyR";
            this.tlSL_CpyR.Size = new System.Drawing.Size(165, 17);
            this.tlSL_CpyR.Text = "CopyRight©2017 by:Me.Mehr";
            // 
            // timer_Net
            // 
            this.timer_Net.Enabled = true;
            this.timer_Net.Interval = 1000;
            // 
            // timerClock
            // 
            this.timerClock.Enabled = true;
            this.timerClock.Interval = 1000;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panel1.Size = new System.Drawing.Size(784, 515);
            this.panel1.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.66277F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.33723F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(782, 513);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 108);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(776, 402);
            this.dataGridView1.TabIndex = 0;
            // 
            // ReportLoh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stsSt_NDCr);
            this.Controls.Add(this.toolSt);
            this.Font = new System.Drawing.Font("B Yekan", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ReportLoh";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReportLoh";
            this.toolSt.ResumeLayout(false);
            this.toolSt.PerformLayout();
            this.stsSt_NDCr.ResumeLayout(false);
            this.stsSt_NDCr.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolSt;
        private System.Windows.Forms.ToolStripLabel lbl_Dte;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lbl_Tme;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lbl_Usr;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel lbl_Lvl;
        private System.Windows.Forms.ToolStripLabel lbl_LvlT;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel lbl_Net;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tSlbl_Exit;
        private System.Windows.Forms.StatusStrip stsSt_NDCr;
        private System.Windows.Forms.ToolStripStatusLabel tlSL_CpyR;
        private System.Windows.Forms.Timer timer_Net;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}