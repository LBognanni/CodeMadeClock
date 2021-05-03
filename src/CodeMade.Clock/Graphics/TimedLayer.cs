using CodeMade.ScriptedGraphics;
using NodaTime;
using System;

namespace CodeMade.Clock
{
    /// <summary>
    /// [reserved]
    /// </summary>
    /// <inheritdoc cref="Layer"/>
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

        /// <summary>
        /// The time zone for this layer.
        /// Can be any of the [valid time zone names in tzdb](https://en.wikipedia.org/wiki/List_of_tz_database_time_zones)
        /// </summary>
        public string TimeZone { get; set; }
        private Lazy<DateTimeZone> ResolvedTimeZone;

        /// <summary>
        /// If set at the default value of `false`, this layer will rotate once at every interval.
        /// If set to `true`, the layer's rotation will be updated more frequently
        /// </summary>
        public bool Smooth { get; set; }

        public void Update(Instant instant)
        {
            Update(instant.InZone(ResolvedTimeZone.Value).TimeOfDay);
        }

        public abstract void Update(LocalTime time);
    }
}
