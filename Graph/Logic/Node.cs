using Graph.Graphics;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Graph.Logic
{
    public enum Status
    {
        Default,
        Current,
        Visited
    }

    public class Node : ILogicElement
    {

        public event EventHandler OnVisitedChange;
        public event EventHandler OnDistanceChange;

        public double X { get; set; }
        public double Y { get; set; }
        public Point Point { get { return new Point(X, Y); } }
        public Status Status { get; private set; }
        public double Distance { get; private set; }
        public Node Previous { get; set; }

        public Node(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Node(Point position)
        {
            X = position.X;
            Y = position.Y;
        }

        public void SetStatus(Status value)
        {
            Status = value;
            OnVisitedChange?.Invoke(this, EventArgs.Empty);
        }

        public void SetStatusWithDelay(Status value)
        {
            SetStatus(value);
            Thread.Sleep(VisualConfig.PauzeTime);
        }

        public void SetDistance(double value)
        {
            Distance = value;
            OnDistanceChange?.Invoke(this, EventArgs.Empty);
        }

        internal Brush GetStatusColorFill()
        {
            var color = Status switch
            {
                Status.Default => VisualConfig.CircleFill,
                Status.Current => VisualConfig.CircleFillCurrent,
                Status.Visited => VisualConfig.CircleFillVisited,
                _ => throw new NotImplementedException(),
            };
            return color;
        }
    }
}
