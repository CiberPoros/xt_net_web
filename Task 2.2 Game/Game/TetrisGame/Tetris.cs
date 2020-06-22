using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CustomCanvas;
using GameInterface;
using TetrisGame.FallingObjects;

namespace TetrisGame
{
    public class Tetris : AbstractGame
    {
        private static readonly AutoResetEvent updateStateWaitHandler = new AutoResetEvent(true);

        public const int TetrisPriorityLimit = 2;
        public const int CellSize = 5;
        public const int GameScoreIncrement = 10;

        private AbstractFallingObject _currentFallingObject;

        public Tetris(int width, int height) : base(width, height, TetrisPriorityLimit)
        {
            if (width % CellSize != 0)
                throw new ArgumentOutOfRangeException(nameof(width), $"Argument {nameof(width)} must be a multiple of {CellSize}.");

            if (height % CellSize != 0)
                throw new ArgumentOutOfRangeException(nameof(height), $"Argument {nameof(height)} must be a multiple of {CellSize}.");

            DelayTime = 5000;

            GridWidth = width / CellSize;
            GridHeight = height / CellSize;
        }

        public int GridWidth { get; }
        public int GridHeight { get; }

        public Point StartFallingObjectPosition { get; }

        public override int DelayTime { get; }

        public override void OnKeyPressed(object sender, ConsoleKeyPressedEventArgs e)
        {
            updateStateWaitHandler.WaitOne();

            bool update = false;
            switch (e.ConsoleKey)
            {
                case ConsoleKey.LeftArrow:
                    update = _currentFallingObject.TryShift(Direction.Letf);
                    break;
                case ConsoleKey.RightArrow:
                    update = _currentFallingObject.TryShift(Direction.Right);
                    break;
                case ConsoleKey.UpArrow:
                    update = _currentFallingObject.TryRotateAroundCenter();
                    break;
                case ConsoleKey.DownArrow:
                    update = _currentFallingObject.TryShift(Direction.Down);
                    break;
            }

            updateStateWaitHandler.Set();

            if (update)
                UpdateConsole();  
        }

        protected override void Init()
        {
            base.Init();

            CreateNewFallingObject();
        }

        protected override List<ConsoleKey> InitHandledKeys()
        {
            return new List<ConsoleKey>()
            {
                ConsoleKey.LeftArrow,
                ConsoleKey.RightArrow,
                ConsoleKey.UpArrow,
                ConsoleKey.DownArrow,
            };
        }

        protected override void Process()
        {
            base.Process();

            UpdateConsole();
        }

        private void OnDropped(object sender, EventArgs e)
        {
            List<int> heightsMas = new List<int>(_currentFallingObject.Figures.Count);
            foreach (var figure in _currentFallingObject.Figures)
            {
                var centerMass = figure.GetCenterMass();
                if (centerMass.Y > Height)
                {
                    IsGameEnded = true;
                    return;
                }

                AddGameObject(new FalledObject(figure.GetLeftTopOfSurroundingRectangle(), this));
                heightsMas.Add(centerMass.Y / CellSize);
            }

            _currentFallingObject.Droped -= OnDropped;
            RemoveGameObject(_currentFallingObject);

            UpdateFullLines(heightsMas.ToArray());

            CreateNewFallingObject();
        }

        private void CreateNewFallingObject()
        {
            _currentFallingObject = FallingObjectCreater.CreateRandom(this);
            _currentFallingObject.Droped += OnDropped;
            AddGameObject(_currentFallingObject);
        }

        private void UpdateFullLines(int[] heightsMas)
        {
            foreach (var height in heightsMas)
            {
                if (IsFullLine(height))
                {
                    var falledObjects = GetGameObjectsByPriority(FalledObject.GetPriority());

                    foreach (var gameObj in falledObjects)
                    {
                        if (gameObj.Center.Y / CellSize > height)
                        {
                            (gameObj as FalledObject).IncreaseFallingTime();
                        }
                    }

                    for (int i = falledObjects.Count - 1; i >= 0; i--)
                    {
                        if (falledObjects[i].Center.Y / CellSize == height)
                        {
                            RemoveGameObject(falledObjects[i]);
                        }
                    }

                    Score += GameScoreIncrement;
                }
            }
        }

        private bool IsFullLine(int height)
        {
            bool[] used = new bool[GridWidth];
            foreach (var gameObj in GetGameObjectsByPriority(FalledObject.GetPriority()))
            {
                if (gameObj.Center.Y / CellSize == height)
                {
                    used[gameObj.Center.X / CellSize] = true;
                }
            }

            return !used.Any(e => e == false);
        }
    }
}
