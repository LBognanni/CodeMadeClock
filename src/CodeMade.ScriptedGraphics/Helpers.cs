using System;
using System.Collections.Generic;

namespace CodeMade.ScriptedGraphics
{
    public static class Helpers
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items) action(item);
        }

        public static void Dispose<T>(this IEnumerable<T> items) where T : IDisposable
        {
            items.ForEach(x => x.Dispose());
        }
    }
}
