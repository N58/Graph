using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Graph
{
    public enum Status
    {
        Standard,
        Current,
        Visited
    }

    public class Node : ILogicElement
    {

        public event EventHandler OnVisitedChanged;
        public event EventHandler OnDistanceChanged;

        public double X { get; set; }
        public double Y { get; set; }
        public Point Point { get { return new Point(X, Y); } }
        public Status Status { get; private set; } = Status.Standard;
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
            OnVisitedChanged?.Invoke(this, EventArgs.Empty);
            Thread.Sleep(1000);
        }

        public void SetDistance(double value)
        {
            Distance = value;
            OnDistanceChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
