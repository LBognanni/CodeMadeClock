using NodaTime;

namespace CodeMade.Clock
{
    public class HoursLayer : TimedLayer
    {
        public float? Angle { get; set; }
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
