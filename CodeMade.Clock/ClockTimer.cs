using NodaTime;
using System;

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
