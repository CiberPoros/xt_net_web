using System;
using System.Collections.Generic;

namespace CustomCanvas
{
    public static class FiguresUtils
    {
        public static Point RotatePoint(Point point, double cos, double sin)
        {
            double x = point.X * cos + point.Y * sin;
            double y = -point.X * sin + point.Y * cos;

            return new Point((int)Math.Round(x), (int)Math.Round(y));
        }

        public static List<Point> CalculatePointsInLine(Point start, Point end)
        {
            int symbolsCount = Math.Max(Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y)) + 1;

            List<Point> result = new List<Point>(symbolsCount);

            double dx = (end.X - start.X + .0) / (symbolsCount - 1);
            double dy = (end.Y - start.Y + .0) / (symbolsCount - 1);

            double incX = 0;
            double incY = 0;

            for (int i = 0; i < symbolsCount; i++)
            {
                result.Add(new Point((int)Math.Round(start.X + incX), (int)Math.Round(start.Y + incY)));

                incX += dx;
                incY += dy;
            }

            return result;
        }

        public static List<Point> CalculatePointsInLineWithoutEnd(Point start, Point end)
        {  
            int symbolsCount = Math.Max(Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y));

            List<Point> result = new List<Point>(symbolsCount);

            double dx = (end.X - start.X + .0) / symbolsCount;
            double dy = (end.Y - start.Y + .0) / symbolsCount;

            double incX = 0;
            double incY = 0;

            for (int i = 0; i < symbolsCount; i++)
            {
                result.Add(new Point((int)Math.Round(start.X + incX), (int)Math.Round(start.Y + incY)));

                incX += dx;
                incY += dy;
            }

            return result;
        }

        public static double CalculateLineLength(Point start, Point end) =>
            Math.Sqrt((end.X - start.X) * (end.X - start.X) + (end.Y - start.Y) * (end.Y - start.Y));

        public static bool OnSameLines(Point point1, Point point2, Point point3) =>
            (point3.X - point1.X + .0) / (point2.X - point1.X) == (point3.Y - point1.Y + .0) / (point2.Y - point1.Y);
    }
}
