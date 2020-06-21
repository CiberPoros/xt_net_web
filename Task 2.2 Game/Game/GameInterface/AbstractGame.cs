using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomCanvas;
using CustomCanvas.Canvases;
using CustomCanvas.Figures;

namespace GameInterface
{
    public abstract class AbstractGame
    {
        protected readonly AbstractCanvas _canvas;
        protected readonly KeyPressedHandler _keyPressedHandler;

        private readonly LinkedList<AbstractGameObject> _controlledGameObjects;

        public AbstractGame(int width, int height)
        {
            TickNumber = 0;
            Score = 0;
            IsGameEnded = false;
            GameSpeed = 10;
            Width = width;
            Height = height;

            _canvas = new ConsoleCanvas(width, height);
            _keyPressedHandler = new KeyPressedHandler(InitHandledKeys());
            _controlledGameObjects = new LinkedList<AbstractGameObject>();
        }

        public abstract int DelayTime { get; }

        public long TickNumber { get; protected set; }
        public int Score { get; protected set; }
        public bool IsGameEnded { get; protected set; }
        public int GameSpeed { get; protected set; }
        public int Width { get; }
        public int Height { get; }

        public virtual void Start()
        {
            Console.CursorVisible = false;

            Task keyHandler = _keyPressedHandler.StartHandleAsync();

            while (!IsGameEnded)
            {
                ProcessHandle();
            }

            Console.CursorVisible = false;
            keyHandler.Wait();
        }

        protected abstract void Process();

        protected abstract List<ConsoleKey> InitHandledKeys();

        protected virtual void AddGameObject(AbstractGameObject gameObject)
        {
            foreach (var figure in gameObject.Figures)
                _canvas.AddFigure(figure);

            _controlledGameObjects.AddLast(gameObject);
        }

        protected virtual void RemoveGameObject(AbstractGameObject gameObject)
        {
            foreach (var figure in gameObject.Figures)
                _canvas.RemoveFigure(figure);

            _controlledGameObjects.Remove(gameObject);
        }

        private void ProcessHandle()
        {
            var awaiter = Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromMilliseconds(DelayTime / GameSpeed));
            });

            Process();

            TickNumber++;

            awaiter.Wait();
        }
    }
}
