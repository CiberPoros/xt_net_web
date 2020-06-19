using CustomCanvas.Figures;

namespace Task2.CustomPaint.ActionWithFigureHelpers
{
    public class GetCenterMassHelper : AbstractActionWithFigureHelper
    {
        protected override string EnterParametersMessage => "";

        protected override int ParametersCount => 0;

        protected override string HandleActionWithFigure(AbstractFigure figure, int[] parameters) => $"Центр масс фигуры: {figure.GetCenterMass()}";
    }
}
