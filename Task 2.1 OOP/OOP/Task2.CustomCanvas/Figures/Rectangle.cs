using System;

namespace CustomCanvas.Figures
{
    public class Rectangle : Polygon
    {
        public Rectangle(Point leftTop, int width, int height, bool contourOnly = false) : base(contourOnly)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width), $"Parameter {nameof(width)} must be positive.");

            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height), $"Parameter {nameof(height)} must be positive.");

            Width = width;
            Height = height;

            _definingPoints.Add(leftTop);
            _definingPoints.Add(new Point(leftTop.X + width, leftTop.Y));
            _definingPoints.Add(new Point(leftTop.X + width, leftTop.Y - height));
            _definingPoints.Add(new Point(leftTop.X, leftTop.Y - height));
        }

        public Point LeftTop => _definingPoints[0];
        public Point RightTop => _definingPoints[1];
        public Point RightBottom => _definingPoints[2];
        public Point LeftBottom => _definingPoints[3];

        public int Width { get; }
        public int Height { get; }
    }
}
