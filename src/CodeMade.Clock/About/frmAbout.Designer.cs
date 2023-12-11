namespace CodeMade.Clock
{
    partial class frmAbout
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
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            pictureBox1 = new System.Windows.Forms.PictureBox();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            label1 = new System.Windows.Forms.Label();
            lblCurrentVersion = new System.Windows.Forms.Label();
            lblNewVersion = new System.Windows.Forms.LinkLabel();
            label2 = new System.Windows.Forms.Label();
            picDonate = new System.Windows.Forms.PictureBox();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            ilLinks = new System.Windows.Forms.ImageList(components);
            label4 = new System.Windows.Forms.Label();
            lnkCodeMade = new System.Windows.Forms.LinkLabel();
            label3 = new System.Windows.Forms.Label();
            linkLabel3 = new System.Windows.Forms.LinkLabel();
            cmdClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picDonate).BeginInit();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBox1.Image = Properties.Resources.logo;
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Padding = new System.Windows.Forms.Padding(8);
            pictureBox1.Size = new System.Drawing.Size(144, 239);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 0);
            tableLayoutPanel1.Controls.Add(cmdClose, 1, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.Size = new System.Drawing.Size(535, 268);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(lblCurrentVersion);
            flowLayoutPanel1.Controls.Add(lblNewVersion);
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(picDonate);
            flowLayoutPanel1.Controls.Add(flowLayoutPanel2);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel1.Location = new System.Drawing.Point(144, 0);
            flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            flowLayoutPanel1.Size = new System.Drawing.Size(391, 239);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            label1.Location = new System.Drawing.Point(11, 0);
            label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(187, 30);
            label1.TabIndex = 1;
            label1.Text = "CodeMade Clock";
            // 
            // lblCurrentVersion
            // 
            lblCurrentVersion.AutoSize = true;
            lblCurrentVersion.Location = new System.Drawing.Point(11, 38);
            lblCurrentVersion.Name = "lblCurrentVersion";
            lblCurrentVersion.Size = new System.Drawing.Size(62, 15);
            lblCurrentVersion.TabIndex = 2;
            lblCurrentVersion.Text = "Version {0}";
            // 
            // lblNewVersion
            // 
            lblNewVersion.AutoSize = true;
            lblNewVersion.Location = new System.Drawing.Point(11, 53);
            lblNewVersion.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            lblNewVersion.Name = "lblNewVersion";
            lblNewVersion.Size = new System.Drawing.Size(288, 15);
            lblNewVersion.TabIndex = 3;
            lblNewVersion.TabStop = true;
            lblNewVersion.Tag = "https://codemade.net";
            lblNewVersion.Text = "A new version is available! Download version {0} here!";
            lblNewVersion.LinkClicked += OpenLinkLabelLink;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(11, 76);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(373, 45);
            label2.TabIndex = 5;
            label2.Text = "CodeMade Clock is free software distributed under the GPL license. If you find this useful, please consider donating by clicking the button below";
            // 
            // picDonate
            // 
            picDonate.Cursor = System.Windows.Forms.Cursors.Hand;
            picDonate.Image = Properties.Resources.bmc_button_128;
            picDonate.Location = new System.Drawing.Point(11, 124);
            picDonate.Name = "picDonate";
            picDonate.Size = new System.Drawing.Size(128, 36);
            picDonate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            picDonate.TabIndex = 4;
            picDonate.TabStop = false;
            picDonate.Tag = "https://www.buymeacoffee.com/codemade";
            picDonate.Click += OpenLink;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.Controls.Add(linkLabel1);
            flowLayoutPanel2.Controls.Add(label4);
            flowLayoutPanel2.Controls.Add(lnkCodeMade);
            flowLayoutPanel2.Controls.Add(label3);
            flowLayoutPanel2.Controls.Add(linkLabel3);
            flowLayoutPanel2.Location = new System.Drawing.Point(8, 171);
            flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new System.Drawing.Size(326, 33);
            flowLayoutPanel2.TabIndex = 6;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            linkLabel1.ImageIndex = 0;
            linkLabel1.ImageList = ilLinks;
            linkLabel1.Location = new System.Drawing.Point(3, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Padding = new System.Windows.Forms.Padding(17, 0, 0, 0);
            linkLabel1.Size = new System.Drawing.Size(60, 15);
            linkLabel1.TabIndex = 0;
            linkLabel1.TabStop = true;
            linkLabel1.Tag = "https://github.com/LBognanni/CodeMadeClock";
            linkLabel1.Text = "Github";
            linkLabel1.LinkClicked += OpenLinkLabelLink;
            // 
            // ilLinks
            // 
            ilLinks.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            ilLinks.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ilLinks.ImageStream");
            ilLinks.TransparentColor = System.Drawing.Color.Transparent;
            ilLinks.Images.SetKeyName(0, "github-icon.png");
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(69, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(10, 15);
            label4.TabIndex = 4;
            label4.Text = "|";
            // 
            // lnkCodeMade
            // 
            lnkCodeMade.AutoSize = true;
            flowLayoutPanel2.SetFlowBreak(lnkCodeMade, true);
            lnkCodeMade.Location = new System.Drawing.Point(85, 0);
            lnkCodeMade.Name = "lnkCodeMade";
            lnkCodeMade.Size = new System.Drawing.Size(178, 15);
            lnkCodeMade.TabIndex = 1;
            lnkCodeMade.TabStop = true;
            lnkCodeMade.Tag = "https://codemade.net";
            lnkCodeMade.Text = "More software @ CodeMade.net";
            lnkCodeMade.LinkClicked += OpenLinkLabelLink;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(3, 18);
            label3.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(222, 15);
            label3.TabIndex = 2;
            label3.Text = "Found a bug? Looking for a new feature?";
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            linkLabel3.ImageIndex = 0;
            linkLabel3.ImageList = ilLinks;
            linkLabel3.Location = new System.Drawing.Point(225, 18);
            linkLabel3.Margin = new System.Windows.Forms.Padding(0, 3, 3, 0);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Padding = new System.Windows.Forms.Padding(17, 0, 0, 0);
            linkLabel3.Size = new System.Drawing.Size(98, 15);
            linkLabel3.TabIndex = 3;
            linkLabel3.TabStop = true;
            linkLabel3.Tag = "https://github.com/LBognanni/CodeMadeClock/issues";
            linkLabel3.Text = "Open an issue";
            linkLabel3.LinkClicked += OpenLinkLabelLink;
            // 
            // cmdClose
            // 
            cmdClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            cmdClose.Location = new System.Drawing.Point(457, 242);
            cmdClose.Name = "cmdClose";
            cmdClose.Size = new System.Drawing.Size(75, 23);
            cmdClose.TabIndex = 3;
            cmdClose.Text = "&Close";
            cmdClose.UseVisualStyleBackColor = true;
            cmdClose.Click += cmdClose_Click;
            // 
            // frmAbout
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            CancelButton = cmdClose;
            ClientSize = new System.Drawing.Size(535, 268);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "frmAbout";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "About CodeMade Clock";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picDonate).EndInit();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCurrentVersion;
        private System.Windows.Forms.LinkLabel lblNewVersion;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.PictureBox picDonate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ImageList ilLinks;
        private System.Windows.Forms.LinkLabel lnkCodeMade;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel3;
    }
}