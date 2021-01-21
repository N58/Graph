using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Graph
{
    class Node : ILogicElement
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Point Point { get { return new Point(X, Y); } }

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
    }
}
