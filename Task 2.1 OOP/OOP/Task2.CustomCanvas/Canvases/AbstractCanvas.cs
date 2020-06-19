using System;
using System.Collections.Generic;
using System.Linq;
using CustomCanvas.Figures;

namespace CustomCanvas.Canvases
{
    public abstract class AbstractCanvas
    {
        private readonly LinkedList<AbstractFigure> _figures;

        public AbstractCanvas(int width, int height)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException(nameof(width), $"Argument {nameof(width)} must be positive.");

            if (height < 0)
                throw new ArgumentOutOfRangeException(nameof(height), $"Argument {nameof(height)} must be positive.");

            _figures = new LinkedList<AbstractFigure>();
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }

        public abstract void Reset();

        public void Clear()
        {
            while (_figures.Count > 0)
            {
                RemoveFigure(_figures.Last());
            }
        }

        public void Update()
        {
            Reset();

            foreach (var figure in _figures)
            {
                UpdateFigureView(figure);
            }
        }

        public void AddFigure(AbstractFigure figure)
        {
            _figures.AddLast(figure);
            figure.OnUpdate += Update;

            Update();
        }

        public void RemoveFigure(AbstractFigure figure)
        {
            _figures.Remove(figure);
            figure.OnUpdate -= Update;

            Update();
        }

        protected abstract void UpdateFigureView(AbstractFigure figure);
    }
}
