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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPreview));
            pbCanvas = new System.Windows.Forms.PictureBox();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            tabControl1 = new System.Windows.Forms.TabControl();
            tpLog = new System.Windows.Forms.TabPage();
            txtLog = new System.Windows.Forms.TextBox();
            tpSettings = new System.Windows.Forms.TabPage();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            dpTime = new System.Windows.Forms.DateTimePicker();
            cbSpecificTime = new System.Windows.Forms.CheckBox();
            cmdSavePreview = new System.Windows.Forms.Button();
            cmbBackground = new System.Windows.Forms.ComboBox();
            cmdCopy = new System.Windows.Forms.Button();
            cbOptimize = new System.Windows.Forms.CheckBox();
            cmbFiles = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)pbCanvas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControl1.SuspendLayout();
            tpLog.SuspendLayout();
            tpSettings.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // pbCanvas
            // 
            pbCanvas.BackgroundImage = (System.Drawing.Image)resources.GetObject("pbCanvas.BackgroundImage");
            pbCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            pbCanvas.Location = new System.Drawing.Point(0, 0);
            pbCanvas.Margin = new System.Windows.Forms.Padding(2);
            pbCanvas.Name = "pbCanvas";
            pbCanvas.Padding = new System.Windows.Forms.Padding(6);
            pbCanvas.Size = new System.Drawing.Size(365, 385);
            pbCanvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pbCanvas.TabIndex = 0;
            pbCanvas.TabStop = false;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainer1.Location = new System.Drawing.Point(0, 23);
            splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(pbCanvas);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tabControl1);
            splitContainer1.Size = new System.Drawing.Size(365, 538);
            splitContainer1.SplitterDistance = 385;
            splitContainer1.SplitterWidth = 3;
            splitContainer1.TabIndex = 1;
            // 
            // tabControl1
            // 
            tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            tabControl1.Controls.Add(tpLog);
            tabControl1.Controls.Add(tpSettings);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point(0, 0);
            tabControl1.Margin = new System.Windows.Forms.Padding(2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(365, 150);
            tabControl1.TabIndex = 1;
            // 
            // tpLog
            // 
            tpLog.Controls.Add(txtLog);
            tpLog.Location = new System.Drawing.Point(4, 4);
            tpLog.Margin = new System.Windows.Forms.Padding(2);
            tpLog.Name = "tpLog";
            tpLog.Padding = new System.Windows.Forms.Padding(2);
            tpLog.Size = new System.Drawing.Size(357, 122);
            tpLog.TabIndex = 0;
            tpLog.Text = "Log";
            tpLog.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            txtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            txtLog.Location = new System.Drawing.Point(2, 2);
            txtLog.Margin = new System.Windows.Forms.Padding(2);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            txtLog.Size = new System.Drawing.Size(353, 118);
            txtLog.TabIndex = 0;
            txtLog.Text = "Loading...";
            // 
            // tpSettings
            // 
            tpSettings.Controls.Add(tableLayoutPanel1);
            tpSettings.Location = new System.Drawing.Point(4, 4);
            tpSettings.Margin = new System.Windows.Forms.Padding(2);
            tpSettings.Name = "tpSettings";
            tpSettings.Padding = new System.Windows.Forms.Padding(2);
            tpSettings.Size = new System.Drawing.Size(357, 122);
            tpSettings.TabIndex = 1;
            tpSettings.Text = "Settings";
            tpSettings.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel1.Controls.Add(dpTime, 1, 0);
            tableLayoutPanel1.Controls.Add(cbSpecificTime, 0, 0);
            tableLayoutPanel1.Controls.Add(cmdSavePreview, 0, 2);
            tableLayoutPanel1.Controls.Add(cmbBackground, 1, 1);
            tableLayoutPanel1.Controls.Add(cmdCopy, 1, 2);
            tableLayoutPanel1.Controls.Add(cbOptimize, 0, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(353, 118);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // dpTime
            // 
            dpTime.Enabled = false;
            dpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            dpTime.Location = new System.Drawing.Point(140, 2);
            dpTime.Margin = new System.Windows.Forms.Padding(2);
            dpTime.Name = "dpTime";
            dpTime.ShowUpDown = true;
            dpTime.Size = new System.Drawing.Size(161, 23);
            dpTime.TabIndex = 3;
            // 
            // cbSpecificTime
            // 
            cbSpecificTime.AutoSize = true;
            cbSpecificTime.Location = new System.Drawing.Point(2, 2);
            cbSpecificTime.Margin = new System.Windows.Forms.Padding(2);
            cbSpecificTime.Name = "cbSpecificTime";
            cbSpecificTime.Size = new System.Drawing.Size(134, 19);
            cbSpecificTime.TabIndex = 0;
            cbSpecificTime.Text = "Show a specific time";
            cbSpecificTime.UseVisualStyleBackColor = true;
            // 
            // cmdSavePreview
            // 
            cmdSavePreview.AutoSize = true;
            cmdSavePreview.Location = new System.Drawing.Point(2, 56);
            cmdSavePreview.Margin = new System.Windows.Forms.Padding(2);
            cmdSavePreview.Name = "cmdSavePreview";
            cmdSavePreview.Size = new System.Drawing.Size(85, 25);
            cmdSavePreview.TabIndex = 4;
            cmdSavePreview.Text = "Save Preview";
            cmdSavePreview.UseVisualStyleBackColor = true;
            cmdSavePreview.Click += cmdSavePreview_Click;
            // 
            // cmbBackground
            // 
            cmbBackground.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbBackground.FormattingEnabled = true;
            cmbBackground.Location = new System.Drawing.Point(140, 29);
            cmbBackground.Margin = new System.Windows.Forms.Padding(2);
            cmbBackground.Name = "cmbBackground";
            cmbBackground.Size = new System.Drawing.Size(122, 23);
            cmbBackground.TabIndex = 5;
            // 
            // cmdCopy
            // 
            cmdCopy.AutoSize = true;
            cmdCopy.Location = new System.Drawing.Point(140, 56);
            cmdCopy.Margin = new System.Windows.Forms.Padding(2);
            cmdCopy.Name = "cmdCopy";
            cmdCopy.Size = new System.Drawing.Size(82, 25);
            cmdCopy.TabIndex = 1;
            cmdCopy.Text = "Copy image";
            cmdCopy.UseVisualStyleBackColor = true;
            cmdCopy.Click += CmdCopy_Click;
            // 
            // cbOptimize
            // 
            cbOptimize.AutoSize = true;
            cbOptimize.Location = new System.Drawing.Point(2, 29);
            cbOptimize.Margin = new System.Windows.Forms.Padding(2);
            cbOptimize.Name = "cbOptimize";
            cbOptimize.Size = new System.Drawing.Size(74, 19);
            cbOptimize.TabIndex = 6;
            cbOptimize.Text = "Optimize";
            cbOptimize.UseVisualStyleBackColor = true;
            // 
            // cmbFiles
            // 
            cmbFiles.Dock = System.Windows.Forms.DockStyle.Top;
            cmbFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbFiles.FormattingEnabled = true;
            cmbFiles.Location = new System.Drawing.Point(0, 0);
            cmbFiles.Margin = new System.Windows.Forms.Padding(2);
            cmbFiles.Name = "cmbFiles";
            cmbFiles.Size = new System.Drawing.Size(365, 23);
            cmbFiles.TabIndex = 2;
            // 
            // frmPreview
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(365, 561);
            Controls.Add(splitContainer1);
            Controls.Add(cmbFiles);
            Margin = new System.Windows.Forms.Padding(2);
            Name = "frmPreview";
            Text = "Preview";
            ((System.ComponentModel.ISupportInitialize)pbCanvas).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tpLog.ResumeLayout(false);
            tpLog.PerformLayout();
            tpSettings.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
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