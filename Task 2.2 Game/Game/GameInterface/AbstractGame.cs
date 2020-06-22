using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CustomCanvas.Canvases;

namespace GameInterface
{
    public abstract class AbstractGame
    {
        protected readonly AbstractCanvas _canvas;
        protected readonly KeyPressedHandler _keyPressedHandler;

        private readonly List<List<AbstractGameObject>> _gameObjectsByPriority;

        public AbstractGame(int width, int height, int priorityLimit)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width), $"Argument {nameof(width)} must be positive.");

            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height), $"Argument {nameof(height)} must be positive.");

            if (priorityLimit < 0)
                throw new ArgumentOutOfRangeException(nameof(priorityLimit), $"Argument {nameof(priorityLimit)} can't be negative.");

            TickNumber = 0;
            Score = 0;
            IsGameEnded = false;
            GameSpeed = 10;
            Width = width;
            Height = height;
            PriorityLimit = priorityLimit;

            _canvas = new ConsoleCanvas(width, height);
            _keyPressedHandler = new KeyPressedHandler(InitHandledKeys());
            _gameObjectsByPriority = new List<List<AbstractGameObject>>(priorityLimit + 1);

            for (int i = 0; i < priorityLimit + 1; i++)
                _gameObjectsByPriority.Add(new List<AbstractGameObject>());

            _keyPressedHandler.KeyPressed += OnKeyPressed;
        }

        public AbstractGame(int width, int height) : this(width, height, 0) { }

        public abstract int DelayTime { get; }

        public long TickNumber { get; protected set; }
        public int Score { get; protected set; }
        public bool IsGameEnded { get; protected set; }

        public int GameSpeed { get; protected set; }
        public int Width { get; }
        public int Height { get; }

        public int PriorityLimit { get; }

        public abstract void OnKeyPressed(object sender, ConsoleKeyPressedEventArgs e);

        public virtual void Start()
        {
            Console.CursorVisible = false;
            Init();

            Thread thread = new Thread(() => _keyPressedHandler.StartHandle());
            thread.Start();

            while (!IsGameEnded)
            {
                ProcessHandle();
            }

            Console.CursorVisible = false;
            _keyPressedHandler.StopHandle();
            thread.Join();
        }

        public IReadOnlyList<AbstractGameObject> GetGameObjectsByPriority(int priority)
        {
            if (priority < 0 || priority > PriorityLimit)
                throw new ArgumentOutOfRangeException(nameof(priority), $"Invalid value of {nameof(priority)}.");

            return _gameObjectsByPriority[priority];
        }

        protected abstract List<ConsoleKey> InitHandledKeys();

        // for create start objects
        protected virtual void Init() { }

        protected virtual void Process()
        {
            for (int i = 0; i < _gameObjectsByPriority.Count; i++)
            {
                for (int j = _gameObjectsByPriority[i].Count - 1; j >= 0; j--)
                {
                    if (_gameObjectsByPriority[i][j] is IProcessable)
                    {
                        (_gameObjectsByPriority[i][j] as IProcessable).Process();
                    }
                }
            }
        }

        protected virtual void AddGameObject(AbstractGameObject gameObject)
        {
            foreach (var figure in gameObject.Figures)
                _canvas.AddFigure(figure);

            if (gameObject is IProcessable)
                _gameObjectsByPriority[(gameObject as IProcessable).Proirity].Add(gameObject);
            else
                _gameObjectsByPriority[0].Add(gameObject);
        }

        protected virtual void RemoveGameObject(AbstractGameObject gameObject)
        {
            foreach (var figure in gameObject.Figures)
                _canvas.RemoveFigure(figure);

            if (gameObject is IProcessable)
                _gameObjectsByPriority[(gameObject as IProcessable).Proirity].Remove(gameObject);
            else
                _gameObjectsByPriority[0].Remove(gameObject);
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
