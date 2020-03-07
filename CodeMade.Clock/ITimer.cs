using NodaTime;

namespace CodeMade.Clock
{
    public interface ITimer
    {
        Instant GetTime();
    }
}
