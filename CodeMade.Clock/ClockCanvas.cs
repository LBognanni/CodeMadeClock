using CodeMade.ScriptedGraphics;

namespace CodeMade.Clock
{
    internal class ClockCanvas : Canvas
    {
        protected ITimer _timer;

        public ClockCanvas(ITimer timer, int width, int height, string backgroundColor) : base(width, height, backgroundColor)
        {
            _timer = timer;
        }
    }
}
