using System;
using System.Collections.Generic;
using CustomCanvas;
using CustomCanvas.Canvases;
using CustomCanvas.Figures;

namespace Game.FallingObjects
{
    public abstract class AbstractFallingObject
    {
        public const int TypesCount = 5;
        public const double RotationAngle = Math.PI / 2;
        protected readonly List<AbstractFigure> _figures;

        private static readonly Random _random = new Random();

        public AbstractFallingObject(Point rotationCenter)
        {
            RotationCenter = rotationCenter;
            _figures = new List<AbstractFigure>(4);
        }

        public Point RotationCenter { get; private set; }

        public static AbstractFallingObject CreateInstance(Point rotationCenter, FallingObjectType fallingObjectType)
        {
            switch (fallingObjectType)
            {
                case FallingObjectType.CrookedStick:
                    return new CrookedStick(rotationCenter);
                case FallingObjectType.Cube:
                    return new Cube(rotationCenter);
                case FallingObjectType.Hanger:
                    return new Hanger(rotationCenter);
                case FallingObjectType.StraightStick:
                    return new StraightStick(rotationCenter);
                case FallingObjectType.Thunder:
                    return new Thunder(rotationCenter);
                default:
                    throw new ArgumentOutOfRangeException(nameof(fallingObjectType), $"Unknown type {nameof(fallingObjectType)}.");
            }
        }

        public static AbstractFallingObject CreateRandom(Point rotationCenter)
        {
            var rnd = _random.Next(TypesCount);

            switch (rnd)
            {
                case 0:
                    return CreateInstance(rotationCenter, FallingObjectType.CrookedStick);
                case 1:
                    return CreateInstance(rotationCenter, FallingObjectType.Cube);
                case 2:
                    return CreateInstance(rotationCenter, FallingObjectType.Hanger);
                case 3:
                    return CreateInstance(rotationCenter, FallingObjectType.StraightStick);
                case 4:
                    return CreateInstance(rotationCenter, FallingObjectType.Thunder);
                default:
                    return null;
            }
        }

        public void AddToCanvas(AbstractCanvas canvas)
        {
            foreach (var figure in _figures)
            {
                canvas.AddFigure(figure);
            }
        }

        public IReadOnlyList<AbstractFigure> Figures => _figures;

        public void Rotate()
        {
            foreach (var figure in _figures)
            {
                figure.RotateAroundOf(RotationCenter, RotationAngle);
            }
        }

        public void Shift(int dx, int dy)
        {
            foreach (var figure in _figures)
            {
                figure.Shift(dx, dy);
            }

            RotationCenter = RotationCenter.Shift(dx, dy);
        }
    }
}
