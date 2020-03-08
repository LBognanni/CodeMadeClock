using NodaTime;
using System;

namespace CodeMade.Clock
{
    public class HoursLayer : TimedLayer
    {
        public bool Is24Hours { get; set; }

        public override void Update(LocalTime time)
        {
            float angle = Is24Hours ? 15.0f : 30.0f;

            if (Smooth)
            {
                Rotate = (float)time.Hour * angle + ((float)time.Minute * (30.0f / 60.0f));
            }
            else
            {
                Rotate = time.Hour * angle;
            }
        }
    }
}
