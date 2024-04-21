using NodaTime;

namespace CodeMade.Clock
{
    /// <summary>
    /// A layer that is only visible every other second
    /// </summary>
    internal class BlinkLayer : TimedLayer
    {
        private bool _visible = true;

        public override void Update(LocalTime time)
        {
            _visible = time.Second % 2 == 0;
        }

        public override void Render(System.Drawing.Graphics g, float scaleFactor = 1)
        {
            if (!_visible)
            {
                return;
            }

            base.Render(g, scaleFactor);
        }
    }
}
