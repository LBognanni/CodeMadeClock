using NodaTime;

namespace CodeMade.Clock
{
    public class SecondsLayer : TimedLayer
    {
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
