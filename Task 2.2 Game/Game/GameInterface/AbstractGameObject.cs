using System.Collections.Generic;
using CustomCanvas;
using CustomCanvas.Figures;

namespace GameInterface
{
    public abstract class AbstractGameObject
    {
        protected readonly List<AbstractFigure> _figures;
        protected readonly AbstractGame _gameState;

        public AbstractGameObject(Point center, AbstractGame gameState)
        {
            _figures = new List<AbstractFigure>();
            _gameState = gameState;

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
