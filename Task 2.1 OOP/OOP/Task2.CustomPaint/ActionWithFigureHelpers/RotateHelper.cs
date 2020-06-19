using System;
using CustomCanvas;
using CustomCanvas.Figures;

namespace Task2.CustomPaint.ActionWithFigureHelpers
{
    public class RotateHelper : AbstractActionWithFigureHelper
    {
        protected override string EnterParametersMessage => 
            "Введите три целых числа через пробел: координата x опорной точки, координата y опорной точки, угол поворота в градусах:";

        protected override int ParametersCount => 3;

        protected override string HandleActionWithFigure(AbstractFigure figure, int[] parameters)
        {
            figure.RotateAroundOf(new Point(parameters[0], parameters[1]), (parameters[2] + .0) / 180 * Math.PI);

            return "Поворот выполнен. Отобразите холст, чтобы увидеть изменения.";
        }
    }
}
