using NodaTime;

namespace CodeMade.Clock
{
    /// <summary>
    /// A layer that rotates every minute
    /// </summary>
    /// <inheritdoc cref="TimedLayer"/>
    public class MinutesLayer : TimedLayer
    {
        /// <summary>
        /// [Optional] the angle of rotation
        /// </summary>
        public float? Angle { get; set; }

        public override void Update(LocalTime time)
        {
            var angle = Angle ?? 6.0f;

            if (Smooth)
            {
                Rotate = time.Minute * angle + (time.Second * angle / 60.0f);
            }
            else
            {
                Rotate = time.Minute * angle;
            }
        }
    }
}
