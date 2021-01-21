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
        private static readonly List<Node> nodes = new List<Node>();
        private static readonly List<Connection> connections = new List<Connection>();

        public static List<Node> Nodes
        {
            get
            {
                return nodes;
            }
        }

        public static List<Connection> Connections
        {
            get
            {
                return connections;
            }
        }

        public static bool IsInCircle(Point position, Node node)
        {
            double a = (position.X - node.X) * (position.X - node.X);
            double b = (position.Y - node.Y) * (position.Y - node.Y);
            double r = VisualData.CircleRadius;
            r *= r;

            if ((a + b) < r)
                return true;
            else
                return false;
        }

        public static Node IsInAnyCircle(Point position)
        {
            foreach (Node node in Nodes)
            {
                if (IsInCircle(position, node))
                    return node;
            }

            return null;
        }

        public static bool ConnectionExist(Node a, Node b)
        {
            foreach (Connection conn in Connections)
                if ((conn.A == a && conn.B == b) || (conn.A == b && conn.B == a))
                    return true;

            return false;
        }
    }
}
