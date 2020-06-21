using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public event ConsoleKeyPressedEventHandler OnKeyPressed = delegate { };

        public void StopHandle() => _cancellationTokenSource.Cancel();

        public async Task StartHandleAsync()
        {
            while (true)
            {
                Task<ConsoleKeyInfo> listener = Task.Run(() => Console.ReadKey(), _cancellationTokenSource.Token);

                await listener;

                OnKeyPressed(this, new ConsoleKeyPressedEventArgs(listener.Result.Key));

                if (_cancellationTokenSource.IsCancellationRequested)
                    return;
            }
        }
    }
}
