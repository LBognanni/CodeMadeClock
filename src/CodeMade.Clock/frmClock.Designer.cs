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
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClock));
            contextMenu = new System.Windows.Forms.ContextMenuStrip(components);
            tsmSettings = new System.Windows.Forms.ToolStripMenuItem();
            sizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            smallerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            largerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            tsmClose = new System.Windows.Forms.ToolStripMenuItem();
            il16 = new System.Windows.Forms.ImageList(components);
            il24 = new System.Windows.Forms.ImageList(components);
            il32 = new System.Windows.Forms.ImageList(components);
            tsmAbout = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            contextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenu
            // 
            contextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { tsmSettings, sizeToolStripMenuItem, toolStripMenuItem1, tsmAbout, toolStripSeparator1, tsmClose });
            contextMenu.Name = "contextMenu";
            contextMenu.Size = new System.Drawing.Size(211, 126);
            // 
            // tsmSettings
            // 
            tsmSettings.Name = "tsmSettings";
            tsmSettings.Size = new System.Drawing.Size(210, 22);
            tsmSettings.Text = "&Settings...";
            tsmSettings.Click += tsmSettings_Click;
            // 
            // sizeToolStripMenuItem
            // 
            sizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { smallerToolStripMenuItem, largerToolStripMenuItem });
            sizeToolStripMenuItem.Name = "sizeToolStripMenuItem";
            sizeToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            sizeToolStripMenuItem.Text = "Size";
            // 
            // smallerToolStripMenuItem
            // 
            smallerToolStripMenuItem.Name = "smallerToolStripMenuItem";
            smallerToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            smallerToolStripMenuItem.Text = "Smaller";
            smallerToolStripMenuItem.Click += SmallerToolStripMenuItem_Click;
            // 
            // largerToolStripMenuItem
            // 
            largerToolStripMenuItem.Name = "largerToolStripMenuItem";
            largerToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            largerToolStripMenuItem.Text = "Larger";
            largerToolStripMenuItem.Click += LargerToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(207, 6);
            // 
            // tsmClose
            // 
            tsmClose.Name = "tsmClose";
            tsmClose.Size = new System.Drawing.Size(210, 22);
            tsmClose.Text = "Exit";
            tsmClose.Click += TsmClose_Click;
            // 
            // il16
            // 
            il16.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            il16.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("il16.ImageStream");
            il16.TransparentColor = System.Drawing.Color.Transparent;
            il16.Images.SetKeyName(0, "close");
            // 
            // il24
            // 
            il24.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            il24.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("il24.ImageStream");
            il24.TransparentColor = System.Drawing.Color.Transparent;
            il24.Images.SetKeyName(0, "close");
            // 
            // il32
            // 
            il32.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            il32.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("il32.ImageStream");
            il32.TransparentColor = System.Drawing.Color.Transparent;
            il32.Images.SetKeyName(0, "close");
            // 
            // tsmAbout
            // 
            tsmAbout.Name = "tsmAbout";
            tsmAbout.Size = new System.Drawing.Size(210, 22);
            tsmAbout.Text = "About CodeMade Clock...";
            tsmAbout.Click += tsmAbout_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(207, 6);
            // 
            // frmClock
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(446, 433);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(2);
            Name = "frmClock";
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            Text = "CodeMade Clock";
            contextMenu.ResumeLayout(false);
            ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem tsmAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

