using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomCanvas.Figures
{
    public class Ring : AbstractFigure
    {
        private readonly Circle _innerCircle;
        private readonly Circle _outerCircle;

        public Ring(Point center, int innerRadius, int outerRadius, bool contourOnly = false)
        {
            if (innerRadius <= 0)
                throw new ArgumentOutOfRangeException(nameof(innerRadius), $"Parameter {nameof(innerRadius)} must be positive.");

            if (outerRadius <= 0)
                throw new ArgumentOutOfRangeException(nameof(outerRadius), $"Parameter {nameof(outerRadius)} must be positive.");

            if (innerRadius >= outerRadius)
                throw new ArgumentOutOfRangeException($"Value of {nameof(innerRadius)} must be less than value of {nameof(outerRadius)}");

            _innerCircle = new Circle(center, innerRadius);
            _outerCircle = new Circle(center, outerRadius);

            InnerRadius = innerRadius;
            OuterRadius = outerRadius;

            _definingPoints.Add(center);

            IsClosed = true;
            ContourOnly = contourOnly;
        }

        public Point Center { get => _definingPoints[0]; private set => _definingPoints[0] = value; }
        public int InnerRadius { get; }
        public int OuterRadius { get; }

        public override double GetArea() => _outerCircle.GetArea() - _innerCircle.GetArea();

        public override bool IsFillerPoint(Point point) => _outerCircle.IsFillerPoint(point) && !_innerCircle.IsFillerPoint(point);

        public override List<Point> GetСontoursPoints() => _outerCircle.GetСontoursPoints().Concat(_innerCircle.GetСontoursPoints()).Distinct().ToList();

        public override double GetContoursLength() => _innerCircle.GetContoursLength() + _outerCircle.GetContoursLength();

        public override Point GetLeftTopOfSurroundingRectangle() => _outerCircle.GetLeftTopOfSurroundingRectangle();

        public override Rectangle GetSurroundingRectangle() => _outerCircle.GetSurroundingRectangle();

        public override void Shift(int dx, int dy)
        {
            base.Shift(dx, dy);

            _innerCircle.Shift(dx, dy);
            _outerCircle.Shift(dx, dy);
        }

        public override void RotateAroundOf(Point rotationsCenter, double angle)
        {
            base.RotateAroundOf(rotationsCenter, angle);

            _innerCircle.RotateAroundOf(rotationsCenter, angle);
            _outerCircle.RotateAroundOf(rotationsCenter, angle);
        }
    }
}
