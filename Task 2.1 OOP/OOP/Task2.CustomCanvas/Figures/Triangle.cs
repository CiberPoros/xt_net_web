using System;

namespace CustomCanvas.Figures
{
    public class Triangle : Polygon
    {
        public Triangle(Point point1, Point point2, Point point3, bool contourOnly = false) : base(contourOnly, point1, point2, point3)
        {
            if (FiguresUtils.OnSameLines(point1, point2, point3))
                throw new ArgumentException("Points can't be on the same straight line.");
        }
    }
}
