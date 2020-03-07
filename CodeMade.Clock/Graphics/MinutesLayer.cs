using NodaTime;
using System;

namespace CodeMade.Clock
{
    public class MinutesLayer : TimedLayer
    {
        public override void Update(LocalTime time)
        {
            if (Smooth)
            {
                Rotate = (float)time.Minute * 6.0f + ((float)time.Second / 10.0f);
            }
            else
            {
                Rotate = time.Minute * 6;
            }
        }
    }
}
