using CustomCanvas;
using CustomCanvas.Figures;

namespace Game.FallingObjects
{
    //----
    //-**-
    //-**-
    //----
    class Cube : AbstractFallingObject
    {
        public Cube(Point rotationCenter) : base(rotationCenter)
        {
            _figures.Add(new Rectangle(rotationCenter.Shift(-TetrisGame.CellSize, TetrisGame.CellSize), TetrisGame.CellSize, TetrisGame.CellSize));
            _figures.Add(new Rectangle(rotationCenter.Shift(0, TetrisGame.CellSize), TetrisGame.CellSize, TetrisGame.CellSize));
            _figures.Add(new Rectangle(rotationCenter.Shift(-TetrisGame.CellSize, 0), TetrisGame.CellSize, TetrisGame.CellSize));
            _figures.Add(new Rectangle(rotationCenter.Shift(0, 0), TetrisGame.CellSize, TetrisGame.CellSize));
        }
    }
}
