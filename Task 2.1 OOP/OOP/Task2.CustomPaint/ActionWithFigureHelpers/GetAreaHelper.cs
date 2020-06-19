using CustomCanvas.Figures;

namespace Task2.CustomPaint.ActionWithFigureHelpers
{
    public class GetAreaHelper : AbstractActionWithFigureHelper
    {
        protected override string EnterParametersMessage => "";

        protected override int ParametersCount => 0;

        protected override string HandleActionWithFigure(AbstractFigure figure, int[] parameters) => $"Площадь фигуры: {figure.GetArea()}";
    }
}
