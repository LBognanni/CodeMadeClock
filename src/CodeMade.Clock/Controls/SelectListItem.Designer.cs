namespace CodeMade.Clock.Controls
{
    partial class SelectListItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.imgChecked = new System.Windows.Forms.PictureBox();
            this.imgItem = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgChecked)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgItem)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.Controls.Add(this.imgChecked, 0, 0);
            this.tlpMain.Controls.Add(this.imgItem, 2, 0);
            this.tlpMain.Controls.Add(this.panel1, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(416, 106);
            this.tlpMain.TabIndex = 0;
            // 
            // imgChecked
            // 
            this.imgChecked.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imgChecked.Location = new System.Drawing.Point(20, 28);
            this.imgChecked.Margin = new System.Windows.Forms.Padding(20);
            this.imgChecked.Name = "imgChecked";
            this.imgChecked.Size = new System.Drawing.Size(50, 50);
            this.imgChecked.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgChecked.TabIndex = 0;
            this.imgChecked.TabStop = false;
            // 
            // imgItem
            // 
            this.imgItem.Location = new System.Drawing.Point(306, 10);
            this.imgItem.Margin = new System.Windows.Forms.Padding(10);
            this.imgItem.Name = "imgItem";
            this.imgItem.Size = new System.Drawing.Size(100, 50);
            this.imgItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgItem.TabIndex = 1;
            this.imgItem.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDescription);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(93, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 2;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoEllipsis = true;
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Location = new System.Drawing.Point(0, 21);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Padding = new System.Windows.Forms.Padding(4);
            this.lblDescription.Size = new System.Drawing.Size(200, 79);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "[Description]";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(4);
            this.lblTitle.Size = new System.Drawing.Size(48, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "[Title]";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Location = new System.Drawing.Point(25, 104);
            this.label3.Margin = new System.Windows.Forms.Padding(60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(356, 2);
            this.label3.TabIndex = 1;
            // 
            // SelectListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tlpMain);
            this.Name = "SelectListItem";
            this.Size = new System.Drawing.Size(416, 106);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgChecked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgItem)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.PictureBox imgChecked;
        private System.Windows.Forms.PictureBox imgItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label3;
    }
}
