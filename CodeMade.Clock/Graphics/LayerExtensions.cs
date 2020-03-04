using CodeMade.ScriptedGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMade.Clock
{
    public static class LayerExtensions
    {
        public static void UpdateLayer(Layer layer, DateTime time)
        {
            if (layer is TimedLayer timedLayer)
            {
                timedLayer.Update(time);
            }

            foreach (var shape in layer.Shapes)
            {
                if(shape is Layer subLayer)
                {
                    UpdateLayer(subLayer, time);
                }
            }
        }
    }
}
