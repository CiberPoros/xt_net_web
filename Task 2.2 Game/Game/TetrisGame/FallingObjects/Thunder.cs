using CustomCanvas;
using CustomCanvas.Figures;
using GameInterface;

namespace TetrisGame.FallingObjects
{
    // -*--
    // -**-
    // --*-
    // ----
    public class Thunder : AbstractFallingObject
    {
        public Thunder(Point center, AbstractGame gameState) : base(center, gameState)
        {
            _figures.Add(new Rectangle(center.Shift(-CellSize, CellSize * 2), CellSize, CellSize));
            _figures.Add(new Rectangle(center.Shift(-CellSize, CellSize), CellSize, CellSize));
            _figures.Add(new Rectangle(center.Shift(0, CellSize), CellSize, CellSize));
            _figures.Add(new Rectangle(center.Shift(0, 0), CellSize, CellSize));
        }
    }
}
