using CustomCanvas;
using CustomCanvas.Figures;
using GameInterface;

namespace TetrisGame.FallingObjects
{
    //--*-
    //-**-
    //--*-
    //----
    public class Hanger : AbstractFallingObject
    {
        public Hanger(Point center, AbstractGame gameState) : base(center, gameState)
        {
            _figures.Add(new Rectangle(center.Shift(0, CellSize * 2), CellSize, CellSize));
            _figures.Add(new Rectangle(center.Shift(0, CellSize), CellSize, CellSize));
            _figures.Add(new Rectangle(center.Shift(0, 0), CellSize, CellSize));
            _figures.Add(new Rectangle(center.Shift(-CellSize, 0), CellSize, CellSize));
        }
    }
}
