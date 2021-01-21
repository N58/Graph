using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    class Connection : ILogicElement
    {
        public Node A { get; set; }
        public Node B { get; set; }
        public double value { get; set; }

        public Connection(Node a, Node b)
        {
            A = a;
            B = b;
            value = Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2));
        }
    }
}
