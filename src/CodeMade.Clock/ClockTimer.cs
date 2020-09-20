using NodaTime;

namespace CodeMade.Clock
{
    public class ClockTimer : ITimer
    {
        public Instant GetTime()
        {
            return SystemClock.Instance.GetCurrentInstant();
        }
    }
}
