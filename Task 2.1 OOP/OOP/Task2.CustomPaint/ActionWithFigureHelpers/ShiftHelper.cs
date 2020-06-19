using CustomCanvas.Figures;

namespace Task2.CustomPaint.ActionWithFigureHelpers
{
    public class ShiftHelper : AbstractActionWithFigureHelper
    {
        protected override string EnterParametersMessage => "Введите два целых числа через пробел: сдвиг по оси X, сдвиг по оси Y:";

        protected override int ParametersCount => 2;

        protected override string HandleActionWithFigure(AbstractFigure figure, int[] parameters)
        {
            figure.Shift(parameters[0], parameters[1]);

            return "Сдвиг выполнен. Отобразите холст, чтобы увидеть изменения.";
        }
    }
}
