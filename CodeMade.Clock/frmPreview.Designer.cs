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
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dpTime = new System.Windows.Forms.DateTimePicker();
            this.cmdCopy = new System.Windows.Forms.Button();
            this.cbSpecificTime = new System.Windows.Forms.CheckBox();
            this.cmdSavePreview = new System.Windows.Forms.Button();
            this.cmbFiles = new System.Windows.Forms.ComboBox();
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
            this.pbCanvas.BackgroundImage = global::CodeMade.Clock.Properties.Resources.generic_bg;
            this.pbCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbCanvas.Location = new System.Drawing.Point(0, 0);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(456, 520);
            this.pbCanvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbCanvas.TabIndex = 0;
            this.pbCanvas.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
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
            this.splitContainer1.Size = new System.Drawing.Size(456, 677);
            this.splitContainer1.SplitterDistance = 520;
            this.splitContainer1.TabIndex = 1;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(442, 228);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "Loading...";
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tpLog);
            this.tabControl1.Controls.Add(this.tpSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(456, 153);
            this.tabControl1.TabIndex = 1;
            // 
            // tpLog
            // 
            this.tpLog.Controls.Add(this.txtLog);
            this.tpLog.Location = new System.Drawing.Point(4, 4);
            this.tpLog.Name = "tpLog";
            this.tpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpLog.Size = new System.Drawing.Size(448, 234);
            this.tpLog.TabIndex = 0;
            this.tpLog.Text = "Log";
            this.tpLog.UseVisualStyleBackColor = true;
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.tableLayoutPanel1);
            this.tpSettings.Location = new System.Drawing.Point(4, 4);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpSettings.Size = new System.Drawing.Size(448, 124);
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
            this.tableLayoutPanel1.Controls.Add(this.cmdCopy, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbSpecificTime, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmdSavePreview, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(442, 118);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // dpTime
            // 
            this.dpTime.Enabled = false;
            this.dpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dpTime.Location = new System.Drawing.Point(166, 3);
            this.dpTime.Name = "dpTime";
            this.dpTime.ShowUpDown = true;
            this.dpTime.Size = new System.Drawing.Size(200, 22);
            this.dpTime.TabIndex = 3;
            // 
            // cmdCopy
            // 
            this.cmdCopy.AutoSize = true;
            this.cmdCopy.Location = new System.Drawing.Point(3, 31);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(103, 29);
            this.cmdCopy.TabIndex = 1;
            this.cmdCopy.Text = "Copy image";
            this.cmdCopy.UseVisualStyleBackColor = true;
            // 
            // cbSpecificTime
            // 
            this.cbSpecificTime.AutoSize = true;
            this.cbSpecificTime.Location = new System.Drawing.Point(3, 3);
            this.cbSpecificTime.Name = "cbSpecificTime";
            this.cbSpecificTime.Size = new System.Drawing.Size(157, 21);
            this.cbSpecificTime.TabIndex = 0;
            this.cbSpecificTime.Text = "Show a specific time";
            this.cbSpecificTime.UseVisualStyleBackColor = true;
            // 
            // cmdSavePreview
            // 
            this.cmdSavePreview.AutoSize = true;
            this.cmdSavePreview.Location = new System.Drawing.Point(3, 66);
            this.cmdSavePreview.Name = "cmdSavePreview";
            this.cmdSavePreview.Size = new System.Drawing.Size(103, 29);
            this.cmdSavePreview.TabIndex = 4;
            this.cmdSavePreview.Text = "Save Preview";
            this.cmdSavePreview.UseVisualStyleBackColor = true;
            this.cmdSavePreview.Click += new System.EventHandler(this.cmdSavePreview_Click);
            // 
            // cmbFiles
            // 
            this.cmbFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiles.FormattingEnabled = true;
            this.cmbFiles.Location = new System.Drawing.Point(0, 0);
            this.cmbFiles.Name = "cmbFiles";
            this.cmbFiles.Size = new System.Drawing.Size(456, 24);
            this.cmbFiles.TabIndex = 2;
            // 
            // frmPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(456, 701);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cmbFiles);
            this.Name = "frmPreview";
            this.Text = "Preview";
            this.Load += new System.EventHandler(this.FrmPreview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
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
    }
}