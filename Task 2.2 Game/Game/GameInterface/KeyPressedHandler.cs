using System;
using System.Collections.Generic;
using System.Threading;

namespace GameInterface
{
    public class ConsoleKeyPressedEventArgs : EventArgs
    { 
        public ConsoleKeyPressedEventArgs(ConsoleKey consoleKey)
        {
            ConsoleKey = consoleKey;
        }

        public ConsoleKey ConsoleKey { get; }
    }

    public delegate void ConsoleKeyPressedEventHandler(object sender, ConsoleKeyPressedEventArgs e);

    public class KeyPressedHandler
    {
        private readonly List<ConsoleKey> _keys;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public KeyPressedHandler(IEnumerable<ConsoleKey> keys)
        {
            _keys = new List<ConsoleKey>();

            foreach (var key in keys)
                _keys.Add(key);

            _cancellationTokenSource = new CancellationTokenSource();
        }

        public event ConsoleKeyPressedEventHandler KeyPressed = delegate { };

        public void StopHandle() => _cancellationTokenSource.Cancel();

        public void StartHandle()
        {
            while (true)
            {
                var keyInfo = Console.ReadKey();

                KeyPressed(this, new ConsoleKeyPressedEventArgs(keyInfo.Key));

                if (_cancellationTokenSource.IsCancellationRequested)
                    return;
            }
        }
    }
}
