using System;
using CustomCanvas.Figures;

namespace Task2.CustomPaint.ActionWithFigureHelpers
{
    public abstract class AbstractActionWithFigureHelper
    {
        private static readonly char[] _separators = new char[] { ' ', ',' };

        private static readonly string _inputErrorMessage =
            $"Некорректный ввод. Повторите попытку...{Environment.NewLine}";

        private static readonly string _unknownErrorMessage =
            $"Что-то пошло не так... Повторите попытку...{Environment.NewLine}";

        protected abstract string EnterParametersMessage { get; }

        protected abstract int ParametersCount { get; }

        public static AbstractActionWithFigureHelper GetFigureCreateHelper(ActionWithFigureType actionType)
        {
            switch (actionType)
            {
                case ActionWithFigureType.GetArea:
                    return new GetAreaHelper();
                case ActionWithFigureType.GetContoursLength:
                    return new GetContoursLengthHelper();
                case ActionWithFigureType.GetCenterMass:
                    return new GetCenterMassHelper();
                case ActionWithFigureType.Shift:
                    return new ShiftHelper();
                case ActionWithFigureType.Rotate:
                    return new RotateHelper();
                default:
                    throw new ArgumentOutOfRangeException(nameof(actionType), $"Unknown type: {actionType}."); ;
            }
        }

        public string ReadAndHandleAction(AbstractFigure figure, string userName)
        {
            if (EnterParametersMessage != string.Empty)
                Console.WriteLine($"{userName}, {EnterParametersMessage}");

            while (true)
            {
                var parametersString = string.Empty;

                if (ParametersCount > 0)
                    parametersString = Console.ReadLine();

                if (!ConvertStringToParametersMass(parametersString, ParametersCount, _separators, out int[] parameters))
                {
                    Console.WriteLine(_inputErrorMessage);
                    continue;
                }

                try
                {
                    return HandleActionWithFigure(figure, parameters);
                }
                catch
                {
                    Console.WriteLine(_unknownErrorMessage);
                }
            }
        }

        protected abstract string HandleActionWithFigure(AbstractFigure figure, int[] parameters);

        private static bool ConvertStringToParametersMass(string str, int cntParams, char[] separators, out int[] result)
        {
            result = new int[cntParams];

            if (cntParams == 0)
                return true;

            string[] temp = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            if (temp.Length != cntParams)
                return false;

            for (int i = 0; i < cntParams; i++)
            {
                if (!int.TryParse(temp[i], out result[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
