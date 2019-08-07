using CodeMade.ScriptedGraphics;
using System;
using System.Windows.Forms;
using System.IO;

namespace CodeMade.Clock
{
    public partial class frmPreview : Form
    {
        private string _fileToWatch;
        private Canvas _canvas;
        private ClockCanvas _clockCanvas;
        private FileSystemWatcher _fsw;

        public frmPreview(string fileToWatch)
        {
            _fileToWatch = fileToWatch;
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.pbCanvas.Click += pbCanvas_Click;
        }

        private void pbCanvas_Click(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void FrmPreview_Load(object sender, EventArgs e)
        {
            _fsw = new FileSystemWatcher(Path.GetDirectoryName(_fileToWatch), Path.GetFileName(_fileToWatch));
            _fsw.Changed += fileToWatch_Changed;
            _fsw.EnableRaisingEvents = true;
            UpdateImage();
        }

        private void fileToWatch_Changed(object sender, FileSystemEventArgs e)
        {
            UpdateImage(true);
        }

        private void UpdateImage(bool alsoLoadCanvas = false)
        {

            this.BeginInvoke((Action)(() =>
            {
                this.txtLog.Text = "OK";
            }));

            try
            {
                if ((_canvas == null) || (alsoLoadCanvas))
                {
                        _canvas = Canvas.Load(_fileToWatch);
                    if(_canvas == null)
                    {
                        return;
                    }
                    _clockCanvas = new ClockCanvas(new ClockTimer(), _canvas);
                }
                _clockCanvas.Update();
                var szx = (float)this.pbCanvas.Width / (float)_canvas.Width;
                var szy = (float)this.pbCanvas.Height / (float)_canvas.Height;
                this.BeginInvoke((Action)(() => {
                    try
                    {
                        this.pbCanvas.Image = _clockCanvas.Render(Math.Min(szx, szy));
                    }
                    catch (Exception ex)
                    {
                        this.txtLog.Text = ex.Message;
                    }
                }));
            }
            catch (Exception ex)
            {
                this.BeginInvoke((Action)(() =>
                {
                    this.txtLog.Text = ex.Message;
                }));
                return;
            }
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            UpdateImage();
        }
    }
}
