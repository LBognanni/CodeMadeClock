using System;

namespace CodeMade.Clock
{
    public class ClockTimer : ITimer
    {
        public DateTime GetTime()
        {
            return DateTime.Now;
        }
    }
}
