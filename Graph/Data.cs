using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Graph
{
    static class Data
    {
        public static Nodes Nodes { get; set; } = new Nodes();

        public static Connections Connections { get; set; } = new Connections();

        public static bool IsInCircle(Point position, Node node)
        {
            double a = (position.X - node.X) * (position.X - node.X);
            double b = (position.Y - node.Y) * (position.Y - node.Y);
            double r = VisualData.CircleRadius;
            r *= r;

            return (a + b) < r;
        }

        public static Node IsInAnyCircle(Point position)
        {
            return Nodes.List.FirstOrDefault(n => IsInCircle(position, n));
        }

        
    }
}
