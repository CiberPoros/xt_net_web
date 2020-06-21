using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomCanvas;
using CustomCanvas.Figures;

namespace GameInterface
{
    public abstract class AbstractGameObject
    {
        protected readonly List<AbstractFigure> _figures;

        public AbstractGameObject(Point center)
        {
            _figures = new List<AbstractFigure>();
            Center = center;
        }

        public Point Center { get; protected set; }
        public IReadOnlyList<AbstractFigure> Figures => _figures;

        public virtual void Shift(int dx, int dy)
        {
            foreach (var figure in _figures)
            {
                figure.Shift(dx, dy);
            }

            Center = Center.Shift(dx, dy);
        }

        public virtual void RotateAroundCenter(double angle)
        {
            foreach (var figure in _figures)
            {
                figure.RotateAroundOf(Center, angle);
            }
        }
    }
}
