using CodeMade.ScriptedGraphics;
using ReactiveUI;
using System;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace CodeMade.Clock.Controls
{
    public class SelectListItemViewModel : ReactiveObject
    {
        private string _title;
        private string _description;
        private Image _image;
        private bool _selected;

        public string Title { get => _title; set => this.RaiseAndSetIfChanged(ref _title, value); }
        public string Description { get => _description; set => this.RaiseAndSetIfChanged(ref _description, value); }
        public Image Image { get => _image; set => this.RaiseAndSetIfChanged(ref _image, value); }
        public bool Selected { get => _selected; set => this.RaiseAndSetIfChanged(ref _selected, value); }


        internal static Image CheckedImage = GetCircle(true);
        internal static Image UncheckedImage = GetCircle(false);
        private readonly ObservableAsPropertyHelper<Image> _checkImage;

        public SelectListItemViewModel()
        {
            _checkImage = this.WhenAnyValue(x => x.Selected)
                .Select(SwitchCheckImage)
                .ToProperty(this, x => x.CheckImage);

            SelectCommand = ReactiveCommand.Create<EventArgs>(_ => this.Selected = true);
        }


        public Image CheckImage => _checkImage.Value;
        public ReactiveCommand<EventArgs, Unit> SelectCommand { get; }

        static Image SwitchCheckImage(bool selected) => selected ? CheckedImage : UncheckedImage;

        private static Image GetCircle(bool @checked)
        {
            Canvas canvas = new Canvas(50, 50, "transparent");
            if (@checked)
            {
                canvas.Add(new CircleShape(24, 24, 24, "#00aa12"));
                canvas.Add(new Shape() { Path = "21,32 13,24 11,26 21,36 39,17 37,15", Color = "white" });
            }
            else
            {
                canvas.Add(new CircleShape(24, 24, 24, "#ddd"));
                canvas.Add(new CircleShape(24, 24, 22, "#f2f2f2"));
            }

            return canvas.Render(1);
        }


    }
}
