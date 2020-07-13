using System;

namespace Logger
{
    public interface ILogger : IDisposable
    {
        event EventHandler<string> Logged;
    }
}
