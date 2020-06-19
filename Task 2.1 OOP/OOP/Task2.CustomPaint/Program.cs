using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomCanvas.Canvases;
using CustomCanvas.Figures;
using Task2.CustomPaint.ActionWithFigureHelpers;
using Task2.CustomPaint.FigureCreateHelpers;

namespace Task2.CustomPaint
{
    class Program
    {
        private static string _userName;

        private static readonly string _enterUserNameMessage = 
            $"Введите имя пользователя (не более 15 русских или латинских символов):{Environment.NewLine}";

        private static readonly string _enterFigureNameMessage =
            $"Введите название фигуры. Название фигуры необходимо для определения фигуры при дейстфии с ней. " +
            $"Не более 15 русских, латинских символов или цифр:{Environment.NewLine}";

        private static readonly string _enterContourOnlyMessage =
            $"Выберите один из двух вариантов:{Environment.NewLine}" +
            $"1. Создать полноценную фигуру{Environment.NewLine}" +
            $"2. Создать контур фигуры{Environment.NewLine}";

        private static readonly string _selectActionMessage = 
            $"Выберите действие{Environment.NewLine}" +
            $"1. Добавить фигуру{Environment.NewLine}" +
            $"2. Действие с фигурой{Environment.NewLine}" +
            $"3. Показать холст{Environment.NewLine}" +
            $"4. Очистить холст{Environment.NewLine}" +
            $"5. Сменить пользователя{Environment.NewLine}" +
            $"6. Закрыть программу{Environment.NewLine}";

        private static readonly string _selectFigureTypeMessage =
            $"Выберите фигуру для создания{Environment.NewLine}" +
            $"1. {nameof(FigureType.Circle)}{Environment.NewLine}" +
            $"2. {nameof(FigureType.Line)}{Environment.NewLine}" +
            $"3. {nameof(FigureType.Rectangle)}{Environment.NewLine}" +
            $"4. {nameof(FigureType.Ring)}{Environment.NewLine}" +
            $"5. {nameof(FigureType.Square)}{Environment.NewLine}" +
            $"6. {nameof(FigureType.Triangle)}{Environment.NewLine}";

        private static readonly string _selectActionWithFigureTypeMessage =
            $"Выберите действие с фигурой{Environment.NewLine}" +
            $"1. {nameof(ActionWithFigureType.GetArea)}{Environment.NewLine}" +
            $"2. {nameof(ActionWithFigureType.GetContoursLength)}{Environment.NewLine}" +
            $"3. {nameof(ActionWithFigureType.GetCenterMass)}{Environment.NewLine}" +
            $"4. {nameof(ActionWithFigureType.Shift)}{Environment.NewLine}" +
            $"5. {nameof(ActionWithFigureType.Rotate)}{Environment.NewLine}";

        private static readonly string _inputErrorMessage = $"Некорректный ввод. Повторите попытку...{Environment.NewLine}";

        private static readonly string _unknownFigureErrorMessage = $"Фигуры с таким именем не существует на холсте. Повторите попытку...{Environment.NewLine}";

        private static readonly string _existsFigureErrorMessage = $"Фигура с таким именем уже существует на холсте. Повторите попытку...{Environment.NewLine}";

        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.SetWindowSize(160, 40);

            AbstractCanvas canvas = new ConsoleCanvas(156, 30);

            Start(canvas);
        }

        private static void Start(AbstractCanvas canvas)
        {
            _userName =  ReadUserName();

            Dictionary<string, AbstractFigure> figuresByName = new Dictionary<string, AbstractFigure>();

            while (true)
            {
                var actionType = ReadActionType();

                switch (actionType)
                {
                    case MainMenuActionType.AddFigure:
                        ReadAndAddFigure(canvas, figuresByName);
                        break;
                    case MainMenuActionType.ActionWithFigure:
                        ReadAndExecuteAction(figuresByName);
                        break;
                    case MainMenuActionType.PrintCanvas:
                        Console.WriteLine(canvas);
                        break;
                    case MainMenuActionType.ClearCanvas:
                        canvas.Clear();
                        figuresByName.Clear();
                        break;
                    case MainMenuActionType.ChangeUser:
                        _userName = ReadUserName();
                        break;
                    case MainMenuActionType.Exit:
                        return;
                    default:
                        break;
                }
            }
        }

        private static void ReadAndAddFigure(AbstractCanvas canvas, Dictionary<string, AbstractFigure> figuresByName)
        {
            var figureType = ReadFigureType();

            var figureCreater = AbstractFigureCreateHelper.GetFigureCreateHelper(figureType);

            var figureName = ReadFigureNameForCreate(figuresByName.Keys.ToArray());

            bool contourOnly = false;
            if (figureCreater.AskContourOnly)
                contourOnly = ReadContourOnly();

            var figure = figureCreater.ReadAndCreate(_userName, contourOnly);

            canvas.AddFigure(figure);

            figuresByName.Add(figureName, figure);

            Console.WriteLine("Фигура добавлена. Отобразите холст, чтобы увидеть изменения.");
        }

        private static void ReadAndExecuteAction(Dictionary<string, AbstractFigure> figuresByName)
        {
            var figureName = ReadFigureNameForAction(figuresByName.Keys.ToArray());
            var figure = figuresByName[figureName];
            var actionWithFigureType = ReadActionWithFigureType();
            var helper = AbstractActionWithFigureHelper.GetFigureCreateHelper(actionWithFigureType);
            var resultString = helper.ReadAndHandleAction(figure, _userName);
            Console.WriteLine(resultString);
        }

        private static bool ReadContourOnly()
        {
            Console.WriteLine($"{_userName}, {_enterContourOnlyMessage}");

            while (true)
            {
                var actionCode = Console.ReadKey();

                Console.WriteLine();

                if (!int.TryParse(actionCode.KeyChar.ToString(), out int actionNumber))
                {
                    Console.WriteLine(_inputErrorMessage);
                    continue;
                }

                switch (actionNumber)
                {
                    case 1:
                        return false;
                    case 2:
                        return true;
                    default:
                        Console.WriteLine(_inputErrorMessage);
                        continue;
                }
            }
        }

        private static string ReadFigureNameForCreate(string[] figureNames)
        {
            Console.WriteLine($"{_userName}, {_enterFigureNameMessage}");

            while (true)
            {
                var name = Console.ReadLine();

                if (name.Length > 15 || name.Any(c => !char.IsLetterOrDigit(c)))
                {
                    Console.WriteLine(_inputErrorMessage);
                    continue;
                }

                if (figureNames.Contains(name))
                {
                    Console.WriteLine(_existsFigureErrorMessage);
                    continue;
                }

                return name;
            }
        }

        private static string ReadFigureNameForAction(string[] figureNames)
        {
            Console.WriteLine($"{_userName}, {_enterFigureNameMessage}");

            while (true)
            {
                var name = Console.ReadLine();

                if (name.Length > 15 || name.Any(c => !char.IsLetterOrDigit(c)))
                {
                    Console.WriteLine(_inputErrorMessage);
                    continue;
                }

                if (!figureNames.Contains(name))
                {
                    Console.WriteLine(_unknownFigureErrorMessage);
                    continue;
                }

                return name;
            }
        }

        private static string ReadUserName()
        {
            Console.WriteLine(_enterUserNameMessage);
            
            while (true)
            {
                var name = Console.ReadLine();

                if (name.Length > 15 || name.Any(c => !char.IsLetter(c)))
                {
                    Console.WriteLine(_inputErrorMessage);
                    continue;
                }

                return name;
            }
        }

        private static MainMenuActionType ReadActionType()
        {
            Console.WriteLine($"{_userName}, {_selectActionMessage}");
            while (true)
            {
                var actionCode = Console.ReadKey();

                Console.WriteLine();

                if (!int.TryParse(actionCode.KeyChar.ToString(), out int actionNumber))
                {
                    Console.WriteLine(_inputErrorMessage);
                    continue;
                }

                switch (actionNumber)
                {
                    case 1:
                        return MainMenuActionType.AddFigure;
                    case 2:
                        return MainMenuActionType.ActionWithFigure;
                    case 3:
                        return MainMenuActionType.PrintCanvas;
                    case 4:
                        return MainMenuActionType.ClearCanvas;
                    case 5:
                        return MainMenuActionType.ChangeUser;
                    case 6:
                        return MainMenuActionType.Exit;
                    default:
                        Console.WriteLine(_inputErrorMessage);
                        continue;
                }
            }
        }

        private static FigureType ReadFigureType()
        {
            Console.WriteLine($"{_userName}, {_selectFigureTypeMessage}");
            while (true)
            {
                var actionCode = Console.ReadKey();

                Console.WriteLine();

                if (!int.TryParse(actionCode.KeyChar.ToString(), out int actionNumber))
                {
                    Console.WriteLine(_inputErrorMessage);
                    continue;
                }

                switch (actionNumber)
                {
                    case 1:
                        return FigureType.Circle;
                    case 2:
                        return FigureType.Line;
                    case 3:
                        return FigureType.Rectangle;
                    case 4:
                        return FigureType.Ring;
                    case 5:
                        return FigureType.Square;
                    case 6:
                        return FigureType.Triangle;
                    default:
                        Console.WriteLine(_inputErrorMessage);
                        continue;
                }
            }
        }

        private static ActionWithFigureType ReadActionWithFigureType()
        {
            Console.WriteLine($"{_userName}, {_selectActionWithFigureTypeMessage}");
            while (true)
            {
                var actionCode = Console.ReadKey();

                Console.WriteLine();

                if (!int.TryParse(actionCode.KeyChar.ToString(), out int actionNumber))
                {
                    Console.WriteLine(_inputErrorMessage);
                    continue;
                }

                switch (actionNumber)
                {
                    case 1:
                        return ActionWithFigureType.GetArea;
                    case 2:
                        return ActionWithFigureType.GetContoursLength;
                    case 3:
                        return ActionWithFigureType.GetCenterMass;
                    case 4:
                        return ActionWithFigureType.Shift;
                    case 5:
                        return ActionWithFigureType.Rotate;
                    default:
                        Console.WriteLine(_inputErrorMessage);
                        continue;
                }
            }
        }
    }
}
