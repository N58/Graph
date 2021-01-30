using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Graph.Modes
{
    public abstract class CanvasModes
    {
        public virtual void Initialize()
        {
            return;
        }

        public abstract void MouseDown(object sender, MouseButtonEventArgs e);

        public abstract string ToString();
    }
}
