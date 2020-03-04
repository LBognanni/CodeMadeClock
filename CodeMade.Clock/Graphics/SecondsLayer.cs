using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.Clock
{
    public class SecondsLayer : TimedLayer
    {
        public override void Update(DateTime time)
        {            
            if(Smooth)
            {
                Rotate = (float)time.Second * 6.0f + (((float)time.Millisecond * 6.0f) / 1000.0f);
            }
            else
            {
                Rotate = time.Second * 6;
            }
        }
    }
}
