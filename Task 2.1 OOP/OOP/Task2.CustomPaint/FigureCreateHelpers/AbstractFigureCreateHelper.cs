using System;
using CustomCanvas.Figures;

namespace Task2.CustomPaint.FigureCreateHelpers
{
    public abstract class AbstractFigureCreateHelper
    {
        private static readonly char[] _separators = new char[] { ' ', ',' };

        private static readonly string _inputErrorMessage =
            $"Некорректный ввод. Повторите попытку...{Environment.NewLine}";

        private static readonly string _unknownErrorMessage =
            $"Что-то пошло не так... Повторите попытку...{Environment.NewLine}";

        public abstract bool AskContourOnly { get; }

        protected abstract string EnterParametersMessage { get; }

        protected abstract string ParseParametersErrorMessage { get; }

        protected abstract int ParametersCount { get; }

        public static AbstractFigureCreateHelper GetFigureCreateHelper(FigureType figureType)
        {
            switch (figureType)
            {
                case FigureType.Circle: 
                    return new CircleCreateHelper();
                case FigureType.Line: 
                    return new LineCreateHelper();
                case FigureType.Rectangle:
                    return new RectangleCreateHelper();
                case FigureType.Ring:
                    return new RingCreateHelper();
                case FigureType.Square:
                    return new SquareCreateHelper();
                case FigureType.Triangle:
                    return new TriangleCreateHelper();
                default:
                    throw new ArgumentOutOfRangeException(nameof(figureType), $"Unknown type: {figureType}.");
            }
        }

        public AbstractFigure ReadAndCreate(string userName, bool contourOnly)
        {
            Console.WriteLine($"{userName}, {EnterParametersMessage}");

            while (true)
            {
                var parametersString = Console.ReadLine();

                if (!ConvertStringToParametersMass(parametersString, ParametersCount, _separators, out int[] parameters))
                {
                    Console.WriteLine(_inputErrorMessage);
                    continue;
                }

                try
                {
                    return CreateFigure(parameters, contourOnly);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine(_inputErrorMessage);
                }
                catch
                {
                    Console.WriteLine(_unknownErrorMessage);
                }
            }
        }

        protected abstract AbstractFigure CreateFigure(int[] parameters, bool contourOnly);

        private static bool ConvertStringToParametersMass(string str, int cntParams, char[] separators, out int[] result)
        {
            string[] temp = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            result = new int[cntParams];

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
