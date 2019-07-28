using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.Clock
{
    public class MinutesLayer : TimedLayer
    {
        public override void Update(DateTime time)
        {
            if (Smooth)
            {
                TransformRotate = (float)time.Minute * 6.0f + ((float)time.Second / 10.0f);
            }
            else
            {
                TransformRotate = time.Minute * 6;
            }
        }
    }
}
