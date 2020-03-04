using System;

namespace CodeMade.Clock
{
    public class HoursLayer : TimedLayer
    {
        public override void Update(DateTime time)
        {
            if (Smooth)
            {
                Rotate = (float)time.Hour * 30.0f + ((float)time.Minute * (30.0f / 60.0f));
            }
            else
            {
                Rotate = time.Hour * 30;
            }
        }
    }
}
