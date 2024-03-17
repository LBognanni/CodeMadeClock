using NodaTime;

namespace CodeMade.Clock
{

    /// <summary>
    /// A layer that rotates every second
    /// </summary>
    /// <inheritdoc cref="TimedLayer"/>
    public class SecondsLayer : TimedLayer
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
                Rotate = time.Second * angle + ((time.Millisecond * angle) / 1000.0f);
            }
            else
            {
                Rotate = time.Second * angle;
            }
        }
    }
}
