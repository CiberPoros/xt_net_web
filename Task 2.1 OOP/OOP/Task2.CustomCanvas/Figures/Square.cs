using System;

namespace CustomCanvas.Figures
{
    public class Square : Polygon
    {
        public Square(Point leftTop, int size, bool contourOnly = false) : base(contourOnly)
        {
            if (size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size), $"Parameter {nameof(size)} must be positive.");

            Size = size;

            _definingPoints.Add(leftTop);
            _definingPoints.Add(new Point(leftTop.X + size, leftTop.Y));
            _definingPoints.Add(new Point(leftTop.X + size, leftTop.Y - size));
            _definingPoints.Add(new Point(leftTop.X, leftTop.Y - size));
        }

        public Point LeftTop => _definingPoints[0];
        public Point RightTop => _definingPoints[1];
        public Point RightBottom => _definingPoints[2];
        public Point LeftBottom => _definingPoints[3];

        public int Size { get; }

        public Rectangle ToRectangle() => new Rectangle(LeftTop, Size, Size, ContourOnly);
    }
}
