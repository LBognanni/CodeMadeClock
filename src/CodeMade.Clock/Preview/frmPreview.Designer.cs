namespace CodeMade.Clock
{
    partial class frmPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPreview));
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dpTime = new System.Windows.Forms.DateTimePicker();
            this.cmdCopy = new System.Windows.Forms.Button();
            this.cbSpecificTime = new System.Windows.Forms.CheckBox();
            this.cmdSavePreview = new System.Windows.Forms.Button();
            this.cmbBackground = new System.Windows.Forms.ComboBox();
            this.cmbFiles = new System.Windows.Forms.ComboBox();
            this.cbOptimize = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpLog.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbCanvas
            // 
            this.pbCanvas.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbCanvas.BackgroundImage")));
            this.pbCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbCanvas.Location = new System.Drawing.Point(0, 0);
            this.pbCanvas.Margin = new System.Windows.Forms.Padding(2);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Padding = new System.Windows.Forms.Padding(6);
            this.pbCanvas.Size = new System.Drawing.Size(365, 384);
            this.pbCanvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbCanvas.TabIndex = 0;
            this.pbCanvas.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 23);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pbCanvas);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(365, 538);
            this.splitContainer1.SplitterDistance = 384;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tpLog);
            this.tabControl1.Controls.Add(this.tpSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(365, 151);
            this.tabControl1.TabIndex = 1;
            // 
            // tpLog
            // 
            this.tpLog.Controls.Add(this.txtLog);
            this.tpLog.Location = new System.Drawing.Point(4, 4);
            this.tpLog.Margin = new System.Windows.Forms.Padding(2);
            this.tpLog.Name = "tpLog";
            this.tpLog.Padding = new System.Windows.Forms.Padding(2);
            this.tpLog.Size = new System.Drawing.Size(357, 123);
            this.tpLog.TabIndex = 0;
            this.tpLog.Text = "Log";
            this.tpLog.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtLog.Location = new System.Drawing.Point(2, 2);
            this.txtLog.Margin = new System.Windows.Forms.Padding(2);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(353, 119);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "Loading...";
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.tableLayoutPanel1);
            this.tpSettings.Location = new System.Drawing.Point(4, 4);
            this.tpSettings.Margin = new System.Windows.Forms.Padding(2);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(2);
            this.tpSettings.Size = new System.Drawing.Size(357, 123);
            this.tpSettings.TabIndex = 1;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.dpTime, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbSpecificTime, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmdSavePreview, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbBackground, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmdCopy, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbOptimize, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(353, 119);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // dpTime
            // 
            this.dpTime.Enabled = false;
            this.dpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dpTime.Location = new System.Drawing.Point(140, 2);
            this.dpTime.Margin = new System.Windows.Forms.Padding(2);
            this.dpTime.Name = "dpTime";
            this.dpTime.ShowUpDown = true;
            this.dpTime.Size = new System.Drawing.Size(161, 23);
            this.dpTime.TabIndex = 3;
            // 
            // cmdCopy
            // 
            this.cmdCopy.AutoSize = true;
            this.cmdCopy.Location = new System.Drawing.Point(140, 56);
            this.cmdCopy.Margin = new System.Windows.Forms.Padding(2);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(82, 25);
            this.cmdCopy.TabIndex = 1;
            this.cmdCopy.Text = "Copy image";
            this.cmdCopy.UseVisualStyleBackColor = true;
            // 
            // cbSpecificTime
            // 
            this.cbSpecificTime.AutoSize = true;
            this.cbSpecificTime.Location = new System.Drawing.Point(2, 2);
            this.cbSpecificTime.Margin = new System.Windows.Forms.Padding(2);
            this.cbSpecificTime.Name = "cbSpecificTime";
            this.cbSpecificTime.Size = new System.Drawing.Size(134, 19);
            this.cbSpecificTime.TabIndex = 0;
            this.cbSpecificTime.Text = "Show a specific time";
            this.cbSpecificTime.UseVisualStyleBackColor = true;
            // 
            // cmdSavePreview
            // 
            this.cmdSavePreview.AutoSize = true;
            this.cmdSavePreview.Location = new System.Drawing.Point(2, 56);
            this.cmdSavePreview.Margin = new System.Windows.Forms.Padding(2);
            this.cmdSavePreview.Name = "cmdSavePreview";
            this.cmdSavePreview.Size = new System.Drawing.Size(85, 25);
            this.cmdSavePreview.TabIndex = 4;
            this.cmdSavePreview.Text = "Save Preview";
            this.cmdSavePreview.UseVisualStyleBackColor = true;
            this.cmdSavePreview.Click += new System.EventHandler(this.cmdSavePreview_Click);
            // 
            // cmbBackground
            // 
            this.cmbBackground.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBackground.FormattingEnabled = true;
            this.cmbBackground.Location = new System.Drawing.Point(140, 29);
            this.cmbBackground.Margin = new System.Windows.Forms.Padding(2);
            this.cmbBackground.Name = "cmbBackground";
            this.cmbBackground.Size = new System.Drawing.Size(122, 23);
            this.cmbBackground.TabIndex = 5;
            // 
            // cmbFiles
            // 
            this.cmbFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiles.FormattingEnabled = true;
            this.cmbFiles.Location = new System.Drawing.Point(0, 0);
            this.cmbFiles.Margin = new System.Windows.Forms.Padding(2);
            this.cmbFiles.Name = "cmbFiles";
            this.cmbFiles.Size = new System.Drawing.Size(365, 23);
            this.cmbFiles.TabIndex = 2;
            // 
            // cbOptimize
            // 
            this.cbOptimize.AutoSize = true;
            this.cbOptimize.Location = new System.Drawing.Point(2, 29);
            this.cbOptimize.Margin = new System.Windows.Forms.Padding(2);
            this.cbOptimize.Name = "cbOptimize";
            this.cbOptimize.Size = new System.Drawing.Size(74, 19);
            this.cbOptimize.TabIndex = 6;
            this.cbOptimize.Text = "Optimize";
            this.cbOptimize.UseVisualStyleBackColor = true;
            // 
            // frmPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(365, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cmbFiles);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmPreview";
            this.Text = "Preview";
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpLog.ResumeLayout(false);
            this.tpLog.PerformLayout();
            this.tpSettings.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCanvas;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ComboBox cmbFiles;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpLog;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.Button cmdCopy;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DateTimePicker dpTime;
        private System.Windows.Forms.CheckBox cbSpecificTime;
        private System.Windows.Forms.Button cmdSavePreview;
        private System.Windows.Forms.ComboBox cmbBackground;
        private System.Windows.Forms.CheckBox cbOptimize;
    }
}