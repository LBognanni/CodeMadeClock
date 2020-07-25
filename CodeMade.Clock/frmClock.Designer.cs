namespace CodeMade.Clock
{
    partial class frmClock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClock));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmClose = new System.Windows.Forms.ToolStripMenuItem();
            this.il16 = new System.Windows.Forms.ImageList(this.components);
            this.il24 = new System.Windows.Forms.ImageList(this.components);
            this.il32 = new System.Windows.Forms.ImageList(this.components);
            this.tsmSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSettings,
            this.sizeToolStripMenuItem,
            this.toolStripMenuItem1,
            this.tsmClose});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(181, 98);
            // 
            // sizeToolStripMenuItem
            // 
            this.sizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smallerToolStripMenuItem,
            this.largerToolStripMenuItem});
            this.sizeToolStripMenuItem.Name = "sizeToolStripMenuItem";
            this.sizeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sizeToolStripMenuItem.Text = "Size";
            // 
            // smallerToolStripMenuItem
            // 
            this.smallerToolStripMenuItem.Name = "smallerToolStripMenuItem";
            this.smallerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.smallerToolStripMenuItem.Text = "Smaller";
            this.smallerToolStripMenuItem.Click += new System.EventHandler(this.SmallerToolStripMenuItem_Click);
            // 
            // largerToolStripMenuItem
            // 
            this.largerToolStripMenuItem.Name = "largerToolStripMenuItem";
            this.largerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.largerToolStripMenuItem.Text = "Larger";
            this.largerToolStripMenuItem.Click += new System.EventHandler(this.LargerToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmClose
            // 
            this.tsmClose.Name = "tsmClose";
            this.tsmClose.Size = new System.Drawing.Size(180, 22);
            this.tsmClose.Text = "Exit";
            this.tsmClose.Click += new System.EventHandler(this.TsmClose_Click);
            // 
            // il16
            // 
            this.il16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il16.ImageStream")));
            this.il16.TransparentColor = System.Drawing.Color.Transparent;
            this.il16.Images.SetKeyName(0, "close");
            // 
            // il24
            // 
            this.il24.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il24.ImageStream")));
            this.il24.TransparentColor = System.Drawing.Color.Transparent;
            this.il24.Images.SetKeyName(0, "close");
            // 
            // il32
            // 
            this.il32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il32.ImageStream")));
            this.il32.TransparentColor = System.Drawing.Color.Transparent;
            this.il32.Images.SetKeyName(0, "close");
            // 
            // tsmSettings
            // 
            this.tsmSettings.Name = "tsmSettings";
            this.tsmSettings.Size = new System.Drawing.Size(180, 22);
            this.tsmSettings.Text = "&Settings...";
            this.tsmSettings.Click += new System.EventHandler(this.tsmSettings_Click);
            // 
            // frmClock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 375);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmClock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ImageList il16;
        private System.Windows.Forms.ImageList il24;
        private System.Windows.Forms.ImageList il32;
        private System.Windows.Forms.ToolStripMenuItem tsmClose;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmSettings;
    }
}

