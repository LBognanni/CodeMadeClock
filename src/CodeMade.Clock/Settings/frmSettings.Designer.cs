namespace CodeMade.Clock
{
    partial class frmSettings
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            flpSkins = new System.Windows.Forms.FlowLayoutPanel();
            label2 = new System.Windows.Forms.Label();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            label1 = new System.Windows.Forms.Label();
            cmbSkinPack = new System.Windows.Forms.ComboBox();
            cmdAddSkinPack = new System.Windows.Forms.Button();
            tabPage2 = new System.Windows.Forms.TabPage();
            picDownload = new System.Windows.Forms.PictureBox();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            label3 = new System.Windows.Forms.Label();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            cmdCancel = new System.Windows.Forms.Button();
            cmdSave = new System.Windows.Forms.Button();
            ilPreview = new System.Windows.Forms.ImageList(components);
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picDownload).BeginInit();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point(0, 0);
            tabControl1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(644, 484);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(flpSkins);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(tableLayoutPanel1);
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            tabPage1.Size = new System.Drawing.Size(636, 456);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Appearance";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // flpSkins
            // 
            flpSkins.AutoScroll = true;
            flpSkins.Dock = System.Windows.Forms.DockStyle.Fill;
            flpSkins.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flpSkins.Location = new System.Drawing.Point(2, 59);
            flpSkins.Name = "flpSkins";
            flpSkins.Size = new System.Drawing.Size(632, 394);
            flpSkins.TabIndex = 4;
            flpSkins.WrapContents = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = System.Windows.Forms.DockStyle.Top;
            label2.Location = new System.Drawing.Point(2, 34);
            label2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            label2.Name = "label2";
            label2.Padding = new System.Windows.Forms.Padding(0, 7, 0, 3);
            label2.Size = new System.Drawing.Size(65, 25);
            label2.TabIndex = 3;
            label2.Text = "Clock face:";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(cmbSkinPack, 1, 0);
            tableLayoutPanel1.Controls.Add(cmdAddSkinPack, 2, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            tableLayoutPanel1.Location = new System.Drawing.Point(2, 3);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            tableLayoutPanel1.Size = new System.Drawing.Size(632, 31);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(4, 8);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(60, 15);
            label1.TabIndex = 0;
            label1.Text = "Skin Pack:";
            // 
            // cmbSkinPack
            // 
            cmbSkinPack.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cmbSkinPack.FormattingEnabled = true;
            cmbSkinPack.Location = new System.Drawing.Point(72, 4);
            cmbSkinPack.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmbSkinPack.Name = "cmbSkinPack";
            cmbSkinPack.Size = new System.Drawing.Size(500, 23);
            cmbSkinPack.TabIndex = 1;
            // 
            // cmdAddSkinPack
            // 
            cmdAddSkinPack.Anchor = System.Windows.Forms.AnchorStyles.Left;
            cmdAddSkinPack.AutoSize = true;
            cmdAddSkinPack.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            cmdAddSkinPack.Location = new System.Drawing.Point(580, 3);
            cmdAddSkinPack.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdAddSkinPack.Name = "cmdAddSkinPack";
            cmdAddSkinPack.Size = new System.Drawing.Size(48, 25);
            cmdAddSkinPack.TabIndex = 2;
            cmdAddSkinPack.Text = "Add...";
            cmdAddSkinPack.UseVisualStyleBackColor = true;
            cmdAddSkinPack.Click += cmdAddSkinPack_Click;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = System.Drawing.Color.White;
            tabPage2.Controls.Add(picDownload);
            tabPage2.Controls.Add(flowLayoutPanel2);
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(636, 456);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Get more skins";
            // 
            // picDownload
            // 
            picDownload.Cursor = System.Windows.Forms.Cursors.Hand;
            picDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            picDownload.Image = Properties.Resources.midnight_express;
            picDownload.Location = new System.Drawing.Point(3, 115);
            picDownload.Name = "picDownload";
            picDownload.Size = new System.Drawing.Size(630, 338);
            picDownload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            picDownload.TabIndex = 3;
            picDownload.TabStop = false;
            picDownload.Click += picDownload_Click;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoScroll = true;
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel2.Controls.Add(label3);
            flowLayoutPanel2.Controls.Add(flowLayoutPanel3);
            flowLayoutPanel2.Controls.Add(label7);
            flowLayoutPanel2.Controls.Add(label8);
            flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new System.Drawing.Size(630, 112);
            flowLayoutPanel2.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(3, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(69, 15);
            label3.TabIndex = 1;
            label3.Text = "Introducing";
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel3.Controls.Add(label4);
            flowLayoutPanel3.Controls.Add(label5);
            flowLayoutPanel3.Controls.Add(label6);
            flowLayoutPanel3.Location = new System.Drawing.Point(3, 18);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new System.Drawing.Size(509, 61);
            flowLayoutPanel3.TabIndex = 0;
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Webdings", 45F);
            label4.Location = new System.Drawing.Point(3, 0);
            label4.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(87, 61);
            label4.TabIndex = 0;
            label4.Text = "t";
            // 
            // label5
            // 
            label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
            label5.Location = new System.Drawing.Point(90, 8);
            label5.Margin = new System.Windows.Forms.Padding(0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(339, 45);
            label5.TabIndex = 1;
            label5.Text = "Midnight Express";
            // 
            // label6
            // 
            label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Webdings", 40F);
            label6.Location = new System.Drawing.Point(429, 3);
            label6.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(77, 54);
            label6.TabIndex = 2;
            label6.Text = "à";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(3, 82);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(592, 15);
            label7.TabIndex = 2;
            label7.Text = "A set of dark clock faces, perfect for your late nights, and the ideal complement for your dark-mode desktop 😎";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(3, 97);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(206, 15);
            label8.TabIndex = 3;
            label8.Text = "Click the preview below to learn more";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(cmdCancel);
            flowLayoutPanel1.Controls.Add(cmdSave);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 484);
            flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(644, 33);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // cmdCancel
            // 
            cmdCancel.Location = new System.Drawing.Point(552, 3);
            cmdCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new System.Drawing.Size(88, 27);
            cmdCancel.TabIndex = 0;
            cmdCancel.Text = "&Cancel";
            cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdSave
            // 
            cmdSave.Location = new System.Drawing.Point(456, 3);
            cmdSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdSave.Name = "cmdSave";
            cmdSave.Size = new System.Drawing.Size(88, 27);
            cmdSave.TabIndex = 1;
            cmdSave.Text = "&Save";
            cmdSave.UseVisualStyleBackColor = true;
            // 
            // ilPreview
            // 
            ilPreview.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            ilPreview.ImageSize = new System.Drawing.Size(128, 128);
            ilPreview.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // frmSettings
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(644, 517);
            Controls.Add(tabControl1);
            Controls.Add(flowLayoutPanel1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            Name = "frmSettings";
            Text = "Settings";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picDownload).EndInit();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSkinPack;
        private System.Windows.Forms.Button cmdAddSkinPack;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.ImageList ilPreview;
        private System.Windows.Forms.FlowLayoutPanel flpSkins;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox picDownload;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}