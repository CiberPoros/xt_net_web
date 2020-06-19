using CustomCanvas;
using CustomCanvas.Figures;

namespace Task2.CustomPaint.FigureCreateHelpers
{
    public class RectangleCreateHelper : AbstractFigureCreateHelper
    {
        public override bool AskContourOnly => true;

        protected override string EnterParametersMessage =>
            "Введите 4 целых числа через пробел: координата x, координата y, ширина, высота:";

        protected override string ParseParametersErrorMessage =>
            $"Ширина и высота должны быть больше нуля. Повторите попытку...";

        protected override int ParametersCount => 4;

        protected override AbstractFigure CreateFigure(int[] parameters, bool contourOnly) => 
            new Rectangle(new Point(parameters[0], parameters[1]), parameters[2], parameters[3], contourOnly);
    }
}
