using CustomCanvas;
using CustomCanvas.Figures;

namespace Task2.CustomPaint.FigureCreateHelpers
{
    public class TriangleCreateHelper : AbstractFigureCreateHelper
    {
        public override bool AskContourOnly => true;

        protected override string EnterParametersMessage =>
            "Введите 6 целых чисел через пробел: x1, y1, x2, y2, x3, y3:";

        protected override string ParseParametersErrorMessage =>
            "Точки не должны лежать на одной прямой. Повторите попытку...";

        protected override int ParametersCount => 6;

        protected override AbstractFigure CreateFigure(int[] parameters, bool contourOnly) => 
            new Triangle(new Point(parameters[0], parameters[1]), new Point(parameters[2], parameters[3]), new Point(parameters[4], parameters[5]), contourOnly);
    }
}
