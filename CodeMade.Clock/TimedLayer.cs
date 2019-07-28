using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.Clock
{
    public abstract class TimedLayer : Layer
    {
        public TimedLayer()
        {
        }

        public bool Smooth { get; set; }

        public abstract void Update(DateTime time);
    }
}
