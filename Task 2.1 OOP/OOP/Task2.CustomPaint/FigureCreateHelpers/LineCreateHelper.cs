using CustomCanvas;
using CustomCanvas.Figures;

namespace Task2.CustomPaint.FigureCreateHelpers
{
    public class LineCreateHelper : AbstractFigureCreateHelper
    {
        public override bool AskContourOnly => false;

        protected override string EnterParametersMessage =>
            "Введите 4 целых числа через пробел: координата x1, координата y1, координата x2, координата y2,:";

        protected override string ParseParametersErrorMessage =>
            "Отрезок должен создаваться по двум разным точкам. Повторите попытку...";

        protected override int ParametersCount => 4;

        protected override AbstractFigure CreateFigure(int[] parameters, bool contourOnly) => 
            new Line(new Point(parameters[0], parameters[1]), new Point(parameters[2], parameters[3]));
    }
}
