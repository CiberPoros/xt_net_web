using CustomCanvas;
using GameInterface;

namespace TetrisGame
{
    public abstract class TetrisGameObject : AbstractGameObject
    {
        public TetrisGameObject(Point center, AbstractGame gameState) : base(center, gameState) { }

        public int CellSize => Tetris.CellSize;

        public override void Shift(int dx, int dy) => base.Shift(dx * CellSize, dy * CellSize);
    }
}
