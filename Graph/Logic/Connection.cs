using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Logic
{
    class Connection : ILogicElement
    {
        public event EventHandler OnConnectionValueChange;

        public Node A { get; set; }
        public Node B { get; set; }
        public double Value { get; private set; }

        public Connection(Node a, Node b)
        {
            A = a;
            B = b;
            SetValue(Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2)));
        }

        public void SetValue(double value)
        {
            Value = value;
            OnConnectionValueChange?.Invoke(this, EventArgs.Empty);
        }
    }
}
