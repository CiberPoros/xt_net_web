using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CustomCanvas;
using CustomCanvas.Canvases;
using CustomCanvas.Figures;
using Game.FallingObjects;

namespace Game
{
    public class TetrisGame
    {
        public const int CellSize = 5;
        public const int GamePointsIncrement = 10;

        private static readonly object _locker = new object();

        private readonly int delayTime = 5000;
        
        private readonly AbstractCanvas _canvas;
        private readonly List<AbstractFigure> _falledObjects;
        private readonly Dictionary<AbstractFigure, int> _dropTime;

        private int _gamePoints = 0;
        private int speed = 10;

        private AbstractFallingObject _fallingObject;

        public TetrisGame(int width, int height)
        {
            Width = width * CellSize + 1;
            Height = height * CellSize;

            GameFieldWidth = width * CellSize * 3 / 4;
            GameFieldHeight = height * CellSize;

            StartFallingObjectPosition = new Point(GameFieldWidth / 2 - (GameFieldWidth / 2) % CellSize, GameFieldHeight + 2 * CellSize);

            _canvas = new ConsoleCanvas(Width, Height);
            _falledObjects = new List<AbstractFigure>();
            _dropTime = new Dictionary<AbstractFigure, int>();

            _canvas.AddFigure(new Line(new Point(GameFieldWidth, 0), new Point(GameFieldWidth, GameFieldHeight)));

            CreateNewFallingObject();
        }

        public Point StartFallingObjectPosition { get; }

        public int Width { get; }
        public int Height { get; }

        public int GameFieldWidth { get; }
        public int GameFieldHeight { get; }

        public int Start()
        {
            Console.CursorVisible = false;

            // TODO: change to async
            var thread = new Thread(() => ReadAndHandleMove());
            thread.Start();

            while (true)
            {
                if (!Process())
                {
                    Console.CursorVisible = false;

                    thread.Abort();
                    thread.Join();

                    return _gamePoints;
                }
            }
        }

        private bool Process()
        {
            var awaiter = Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromMilliseconds(delayTime / speed));
            });

            UpdateDrops();

            if (!LowerFallingObject())
            {
                HandleDrop(out bool isLose);

                if (isLose)
                    return false;

                CreateNewFallingObject();
            }

            awaiter.Wait();
            return true;
        }

        private void ReadAndHandleMove()
        {
            while (true)
            {
                var keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        ShiftFallingObject(-CellSize, 0);
                        break;
                    case ConsoleKey.RightArrow:
                        ShiftFallingObject(CellSize, 0);
                        break;
                    case ConsoleKey.DownArrow:
                        ShiftFallingObject(0, -CellSize);
                        break;
                    case ConsoleKey.UpArrow:
                        RotateFallingObject();
                        break;
                }   
            }
        }

        private void HandleDrop(out bool isLose)
        {
            isLose = false;

            foreach (var figure in _fallingObject.Figures)
            {
                Point centerMass = figure.GetCenterMass();

                if (centerMass.Y > GameFieldHeight)
                {
                    isLose = true;
                    return;
                }

                _falledObjects.Add(figure);
            }

            UpdateRemovableLines();
        }

        private void UpdateDrops()
        {
            List<AbstractFigure> toRemove = new List<AbstractFigure>();

            foreach (var figure in _dropTime.Keys.ToArray())
            {
                figure.Shift(0, -CellSize);

                _dropTime[figure]--;

                if (_dropTime[figure] == 0)
                {
                    toRemove.Add(figure);
                }
            }

            foreach (var figure in toRemove)
            {
                _dropTime.Remove(figure);
            }
        }

        // TODO: change this...
        private void UpdateRemovableLines()
        {
            int width = GameFieldWidth / CellSize;
            int height = GameFieldHeight / CellSize;

            bool[,] used = new bool[width, height];

            foreach (var falledObj in _falledObjects)
            {
                var center = falledObj.GetCenterMass();

                used[center.X / CellSize, center.Y / CellSize] = true;
            }

            bool update = false;
            for (int i = 0; i < height; i++)
            {
                bool remove = true;

                for (int j = 0; j < width; j++)
                {
                    if (!used[j, i])
                    {
                        remove = false;
                        break;
                    }
                }

                update = update || remove;

                if (remove)
                {
                    _gamePoints += GamePointsIncrement;

                    if (_gamePoints > 0 && _gamePoints % 50 == 0)
                        speed++;

                    for (int k = _falledObjects.Count - 1; k >= 0; k--)
                    {
                        var center = _falledObjects[k].GetCenterMass();

                        if (center.Y / CellSize == i)
                        {
                            _canvas.RemoveFigure(_falledObjects[k]);
                            _falledObjects.RemoveAt(k);
                        }
                        else if (center.Y / CellSize > i)
                        {
                            if (_dropTime.ContainsKey(_falledObjects[k]))
                                _dropTime[_falledObjects[k]]++;
                            else
                                _dropTime.Add(_falledObjects[k], 1);
                        }
                    }
                }   
            }

            if (update)
            {
                UpdateConsole();
            }
        }

        #region SHIFTING
        // return - shifted or not
        private bool ShiftFallingObject(int dx, int dy)
        {
            lock (_locker)
            {
                if (!CheckCanShift(dx, dy))
                    return false;

                _fallingObject.Shift(dx, dy);

                UpdateConsole();

                return true;
            }  
        }

        private bool CheckCanShift(int dx, int dy)
        {
            foreach (var figure in _fallingObject.Figures)
            {
                Point centerMass = figure.GetCenterMass();
                centerMass = centerMass.Shift(dx, dy);

                if (centerMass.X < 0 || centerMass.X > GameFieldWidth || centerMass.Y < 0)
                {
                    return false;
                }

                foreach (var falledObj in _falledObjects)
                {
                    if (falledObj.IsFillerPoint(centerMass))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion

        // return lower or not
        private bool LowerFallingObject() => ShiftFallingObject(0, -CellSize);

        #region ROTATION
        // return rotated or not
        private bool RotateFallingObject()
        {
            lock (_locker)
            {
                double cos = Math.Cos(AbstractFallingObject.RotationAngle);
                double sin = Math.Sin(AbstractFallingObject.RotationAngle);

                if (!CheckCanRotate(cos, sin))
                    return false;

                _fallingObject.Rotate();

                UpdateConsole();

                return true; 
            }
        }

        private bool CheckCanRotate(double cos, double sin)
        {
            foreach (var figure in _fallingObject.Figures)
            {
                Point centerMass = figure.GetCenterMass();
                centerMass = FiguresUtils.RotatePoint(centerMass - _fallingObject.RotationCenter, cos, sin) + _fallingObject.RotationCenter;

                if (centerMass.X < 0 || centerMass.X > GameFieldWidth || centerMass.Y < 0)
                {
                    return false;
                }

                foreach (var falledObj in _falledObjects)
                {
                    if (falledObj.IsFillerPoint(centerMass))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion

        private void UpdateConsole()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(_canvas);
        }

        private void CreateNewFallingObject()
        {
            _fallingObject = AbstractFallingObject.CreateRandom(StartFallingObjectPosition);
            _fallingObject.AddToCanvas(_canvas);
        }
    }
}
