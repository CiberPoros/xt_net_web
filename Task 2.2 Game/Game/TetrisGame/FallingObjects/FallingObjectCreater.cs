using System;
using CustomCanvas;
using GameInterface;

namespace TetrisGame.FallingObjects
{
    public class FallingObjectCreater
    {
        public const int TypesCount = 5;

        private static readonly Random _random = new Random();

        public static AbstractFallingObject CreateInstance(AbstractGame gameState, FallingObjectType fallingObjectType)
        {
            var startFallingObjectRotationCenter = new Point
                (gameState.Width / 2 - (gameState.Width / 2) % Tetris.CellSize, gameState.Height + Tetris.CellSize);

            switch (fallingObjectType)
            {
                case FallingObjectType.CrookedStick:
                    return new CrookedStick(startFallingObjectRotationCenter, gameState);
                case FallingObjectType.Cube:
                    return new Cube(startFallingObjectRotationCenter, gameState);
                case FallingObjectType.Hanger:
                    return new Hanger(startFallingObjectRotationCenter, gameState);
                case FallingObjectType.StraightStick:
                    return new StraightStick(startFallingObjectRotationCenter, gameState);
                case FallingObjectType.Thunder:
                    return new Thunder(startFallingObjectRotationCenter, gameState);
                default:
                    throw new ArgumentOutOfRangeException(nameof(fallingObjectType), $"Unknown type {nameof(fallingObjectType)}.");
            }
        }

        public static AbstractFallingObject CreateRandom(AbstractGame gameState)
        {
            var rnd = _random.Next(TypesCount);

            switch (rnd)
            {
                case 0:
                    return CreateInstance(gameState, FallingObjectType.CrookedStick);
                case 1:
                    return CreateInstance(gameState, FallingObjectType.Cube);
                case 2:
                    return CreateInstance(gameState, FallingObjectType.Hanger);
                case 3:
                    return CreateInstance(gameState, FallingObjectType.StraightStick);
                case 4:
                    return CreateInstance(gameState, FallingObjectType.Thunder);
                default:
                    return null;
            }
        }
    }
}
