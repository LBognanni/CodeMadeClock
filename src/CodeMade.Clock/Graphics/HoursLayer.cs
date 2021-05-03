using NodaTime;

namespace CodeMade.Clock
{
    /// <summary>
    /// A layer that rotates every hour
    /// </summary>
    /// <inheritdoc cref="TimedLayer"/>
    /// <example>
	///	{	
	///	    "$type": "HoursLayer",
    ///     "Shapes": [
    ///      // ... shapes ...
    ///     ]
	///	},
    /// </example>
    public class HoursLayer : TimedLayer
    {
        /// <summary>
        /// [Optional] The angle this layer will rotate by at the hour interval
        /// </summary>
        public float? Angle { get; set; }

        /// <summary>
        /// Only used if `Angle` is not specified.
        /// If true, the layer will complete a full rotation in 24 hours, otherwise it will do so in 12 hours.
        /// Default value is `false`
        /// </summary>
        public bool Is24Hours { get; set; }

        public override void Update(LocalTime time)
        {
            var angle = Angle ?? (Is24Hours ? 15.0f : 30.0f);

            if (Smooth)
            {
                Rotate = time.Hour * angle + (time.Minute * (angle / 60.0f));
            }
            else
            {
                Rotate = time.Hour * angle;
            }
        }
    }
}
