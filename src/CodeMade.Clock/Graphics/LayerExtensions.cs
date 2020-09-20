using CodeMade.ScriptedGraphics;
using NodaTime;

namespace CodeMade.Clock
{
    public static class LayerExtensions
    {
        public static void UpdateLayer(Layer layer, Instant time)
        {
            if (layer is TimedLayer timedLayer)
            {
                timedLayer.Update(time);
            }

            foreach (var shape in layer.Shapes)
            {
                if (shape is Layer subLayer)
                {
                    UpdateLayer(subLayer, time);
                }
            }
        }
    }
}
