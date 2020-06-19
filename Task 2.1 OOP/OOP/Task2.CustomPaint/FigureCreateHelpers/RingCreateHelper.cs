using CustomCanvas;
using CustomCanvas.Figures;

namespace Task2.CustomPaint.FigureCreateHelpers
{
    public class RingCreateHelper : AbstractFigureCreateHelper
    {
        public override bool AskContourOnly => true;

        protected override string EnterParametersMessage =>
            "Введите 4 целых числа через пробел: координата x, координата y, внутренний радиус, внешний радиус:";

        protected override string ParseParametersErrorMessage =>
            "Внутренниы и внешний радиусы должны быть больше нуля. Внешний радиус должен быть больше внутреннего. Повторите попытку...";

        protected override int ParametersCount => 4;

        protected override AbstractFigure CreateFigure(int[] parameters, bool contourOnly) => 
            new Ring(new Point(parameters[0], parameters[1]), parameters[2], parameters[3], contourOnly);
    }
}
