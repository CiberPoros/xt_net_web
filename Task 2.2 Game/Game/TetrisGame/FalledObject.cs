using CustomCanvas;
using CustomCanvas.Figures;
using GameInterface;

namespace TetrisGame
{
    public class FalledObject : TetrisGameObject, IProcessable
    {
        private int _fallingTime;
        private static int _priority;

        public FalledObject(Point center, AbstractGame gameState) : base(center, gameState)
        {
            _figures.Add(new Rectangle(
                new Point(center.X - center.X % CellSize, center.Y - center.Y % CellSize), CellSize, CellSize));

            _fallingTime = 0;
            _priority = 1;
        }

        public static int GetPriority() => _priority;

        public int Proirity => GetPriority();

        public void IncreaseFallingTime() => _fallingTime++;

        public void Process()
        {
            if (_fallingTime > 0)
            {
                Shift(0, -1);
                --_fallingTime;
            }
        }
    }
}
