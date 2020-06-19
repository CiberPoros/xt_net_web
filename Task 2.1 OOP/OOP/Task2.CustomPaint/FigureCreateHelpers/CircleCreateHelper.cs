using CustomCanvas;
using CustomCanvas.Figures;

namespace Task2.CustomPaint.FigureCreateHelpers
{
    public class CircleCreateHelper : AbstractFigureCreateHelper
    {
        public override bool AskContourOnly => true;

        protected override string EnterParametersMessage => 
            "Введите 3 целых числа через пробел: координата x, координата y, радиус:";

        protected override string ParseParametersErrorMessage =>
            "Радиус должен быть больше нуля. Повторите попытку...";

        protected override int ParametersCount => 3;

        protected override AbstractFigure CreateFigure(int[] parameters, bool contourOnly) => 
            new Circle(new Point(parameters[0], parameters[1]), parameters[2], contourOnly);
    }
}
