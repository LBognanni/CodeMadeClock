using CodeMade.ScriptedGraphics;
using NodaTime;
using NodaTime.Extensions;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace CodeMade.Clock
{
    public partial class frmPreview : Form, ITimer
    {
        private string _fileToWatch;
        private Canvas _canvas;
        private ClockCanvas _clockCanvas;
        private FileSystemWatcher _fsw;

        public frmPreview(string fileToWatch)
        {
            _fileToWatch = fileToWatch;
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            pbCanvas.Click += pbCanvas_Click;
            UpdateFileList();

            cbSpecificTime.CheckedChanged += (s, e) => { dpTime.Enabled = cbSpecificTime.Checked; UpdateImage(); };
            dpTime.ValueChanged += (s, e) => { UpdateImage(); };
        }

        private void UpdateFileList()
        {
            cmbFiles.SelectedIndexChanged -= CmbFiles_SelectedIndexChanged;
            string path = Path.GetDirectoryName(_fileToWatch);
            cmbFiles.DataSource = Directory.GetFiles(path, "*.json");
            cmbFiles.Text = _fileToWatch;
            cmbFiles.SelectedIndexChanged += CmbFiles_SelectedIndexChanged;
        }

        private void CmbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            _fileToWatch = cmbFiles.SelectedValue.ToString();
            UpdateWatcherAndImage();
        }

        private void UpdateWatcherAndImage()
        {
            if (_fsw != null)
            {
                _fsw.EnableRaisingEvents = false;
                _fsw.Dispose();
                _fsw = null;
            }
            _fsw = new FileSystemWatcher(Path.GetDirectoryName(_fileToWatch), Path.GetFileName(_fileToWatch));
            _fsw.Changed += fileToWatch_Changed;
            _fsw.EnableRaisingEvents = true;
            UpdateImage(true);
            UpdateFileList();
        }

        private void pbCanvas_Click(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void FrmPreview_Load(object sender, EventArgs e)
        {
            UpdateWatcherAndImage();
        }

        private void fileToWatch_Changed(object sender, FileSystemEventArgs e)
        {
            UpdateImage(true);
        }

        private void UpdateImage(bool alsoLoadCanvas = false)
        {

            BeginInvoke((Action)(() =>
            {
                txtLog.Text = "OK";
            }));

            try
            {
                if ((_canvas == null) || (alsoLoadCanvas))
                {
                    _canvas = Canvas.Load(_fileToWatch);
                    if (_canvas == null)
                    {
                        return;
                    }
                    _clockCanvas = new ClockCanvas(this, _canvas);
                }
                _clockCanvas.Update();
                var szx = (float)pbCanvas.Width / (float)_canvas.Width;
                var szy = (float)pbCanvas.Height / (float)_canvas.Height;
                BeginInvoke((Action)(() =>
                {
                    try
                    {
                        pbCanvas.Image = _clockCanvas.Render(Math.Min(szx, szy));
                    }
                    catch (Exception ex)
                    {
                        txtLog.Text = ex.Message;
                    }
                }));
            }
            catch (Exception ex)
            {
                BeginInvoke((Action)(() =>
                {
                    txtLog.Text = ex.Message;
                }));
                return;
            }
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            UpdateImage();
        }
        private void CmdCopy_Click(object sender, System.EventArgs e)
        {
            Clipboard.Clear();
            DataObject data = new DataObject();
            using (var ms = new MemoryStream())
            {
                pbCanvas.Image.Save(ms, ImageFormat.Png);
                data.SetData("PNG", false, ms);
                Clipboard.SetDataObject(data, true);
            }
        }

        public Instant GetTime()
        {
            if (cbSpecificTime.Checked)
            {
                return dpTime.Value.ToUniversalTime().ToInstant();
            }
            return SystemClock.Instance.GetCurrentInstant();
        }

        private void cmdSavePreview_Click(object sender, EventArgs e)
        {
            string fileName = Path.ChangeExtension(_fileToWatch, "png");
            pbCanvas.Image.Save(fileName);
        }
    }
}
