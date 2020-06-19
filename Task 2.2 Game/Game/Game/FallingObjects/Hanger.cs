using CustomCanvas;
using CustomCanvas.Figures;

namespace Game.FallingObjects
{
    //--*-
    //-**-
    //--*-
    //----
    public class Hanger : AbstractFallingObject
    {
        public Hanger(Point rotationCenter) : base(rotationCenter)
        {
            _figures.Add(new Rectangle(rotationCenter.Shift(0, TetrisGame.CellSize * 2), TetrisGame.CellSize, TetrisGame.CellSize));
            _figures.Add(new Rectangle(rotationCenter.Shift(0, TetrisGame.CellSize), TetrisGame.CellSize, TetrisGame.CellSize));
            _figures.Add(new Rectangle(rotationCenter.Shift(-TetrisGame.CellSize, TetrisGame.CellSize), TetrisGame.CellSize, TetrisGame.CellSize));
            _figures.Add(new Rectangle(rotationCenter.Shift(0, 0), TetrisGame.CellSize, TetrisGame.CellSize));
        }
    }
}
