using CodeMade.ScriptedGraphics;
using NodaTime;
using System;

namespace CodeMade.Clock
{
    public abstract class TimedLayer : Layer
    {
        public TimedLayer()
        {
            ResolvedTimeZone = new Lazy<DateTimeZone>(() =>
            {
                if (string.IsNullOrEmpty(TimeZone))
                    return DateTimeZoneProviders.Tzdb.GetSystemDefault();
                return DateTimeZoneProviders.Tzdb.GetZoneOrNull(TimeZone) ?? DateTimeZoneProviders.Tzdb.GetSystemDefault();
            });
        }

        public string TimeZone { get; set; }
        private Lazy<DateTimeZone> ResolvedTimeZone;

        public bool Smooth { get; set; }

        public void Update(Instant instant)
        {
            Update(instant.InZone(ResolvedTimeZone.Value).TimeOfDay);
        }

        public abstract void Update(LocalTime time);
    }
}
