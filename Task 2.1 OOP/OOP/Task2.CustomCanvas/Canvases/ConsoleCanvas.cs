using System;
using System.Collections.Generic;
using System.Text;
using CustomCanvas.Figures;

namespace CustomCanvas.Canvases
{
    public class ConsoleCanvas : AbstractCanvas
    {
        private const char SpaceSymbol = ' ';
        private const char FillerSymbol = '\'';
        private const char СontourSymbol = '*';
        private const char WallSymbol = '$';

        private readonly char[,] _points;

        public ConsoleCanvas(int width, int height) : base(width, height)
        {
            _points = new char[width, height];

            Reset();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < Width + 2; i++)
                stringBuilder.Append(WallSymbol);
            stringBuilder.Append(Environment.NewLine);

            for (int i = Height - 1; i >= 0; i--)
            {
                stringBuilder.Append(WallSymbol);
                for (int j = 0; j < Width; j++)
                {
                    stringBuilder.Append(_points[j, i]);
                }
                stringBuilder.Append(WallSymbol);

                stringBuilder.Append(Environment.NewLine);
            }

            for (int i = 0; i < Width + 2; i++)
                stringBuilder.Append(WallSymbol);
            stringBuilder.Append(Environment.NewLine);

            return stringBuilder.ToString();
        }

        public override string[] ToStringArray()
        {
            List<string> result = new List<string>();

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < Width + 2; i++)
                stringBuilder.Append(WallSymbol);

            result.Add(stringBuilder.ToString());

            for (int i = Height - 1; i >= 0; i--)
            {
                stringBuilder.Clear();
                stringBuilder.Append(WallSymbol);
                for (int j = 0; j < Width; j++)
                {
                    stringBuilder.Append(_points[j, i]);
                }
                stringBuilder.Append(WallSymbol);

                result.Add(stringBuilder.ToString());
            }

            stringBuilder.Clear();
            for (int i = 0; i < Width + 2; i++)
                stringBuilder.Append(WallSymbol);

            result.Add(stringBuilder.ToString());
            return result.ToArray();
        }

        public override void Reset()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    _points[i, j] = SpaceSymbol;
                }
            }
        }

        protected override void UpdateFigureView(AbstractFigure figure)
        {
            UpdateFillerOfFigure(figure);

            var contouesPoints = figure.GetСontoursPoints();

            foreach (var point in contouesPoints)
            {
                SetSymbol(СontourSymbol, point);
            }
        }

        private void UpdateFillerOfFigure(AbstractFigure figure)
        {
            var points = figure.GetFillerPoints();

            foreach (var point in points)
                SetSymbol(FillerSymbol, point);
        }

        private void SetSymbol(char symbol, int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return;

            if (GetSymbolPriority(_points[x, y]) < GetSymbolPriority(symbol))
            {
                _points[x, y] = symbol;
            }
        }

        private void SetSymbol(char symbol, Point point) => SetSymbol(symbol, point.X, point.Y);

        private static int GetSymbolPriority(char symbol)
        {
            switch (symbol)
            {
                case SpaceSymbol: return 0;
                case FillerSymbol: return 1;
                case СontourSymbol: return 2;
                default: throw new ArgumentOutOfRangeException(nameof(symbol), $"Unknown symbol {nameof(symbol)}");
            }
        }
    }
}
