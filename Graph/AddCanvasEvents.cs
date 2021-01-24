using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Graph
{
    class AddCanvasEvents : CanvasEvents
    {
        private static readonly AddCanvasEvents instance = new AddCanvasEvents();

        public static AddCanvasEvents Instance
        {
            get
            {
                return instance;
            }
        }

        public static event EventHandler<OnNodeEventArgs> OnNodeAdded;
        public class OnNodeEventArgs : EventArgs
        {
            public Canvas canvas;
            public Node node;
        }

        public override void MouseDown(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = (Canvas)sender;
            Point pos = Mouse.GetPosition(canvas);
            Node node = new Node(pos);

            if (pos.X + (VisualData.CircleRadius / 2) > canvas.Width ||
                pos.X - (VisualData.CircleRadius / 2) < 0 ||
                pos.Y + (VisualData.CircleRadius / 2) > canvas.Height ||
                pos.Y - (VisualData.CircleRadius / 2) < 0)
                return;

            foreach (Node item in Data.Nodes.List)
                if (Data.IsInCircle(pos, item))
                    return;

            Data.Nodes.List.Add(node);
            OnNodeAdded?.Invoke(this, new OnNodeEventArgs { canvas = canvas, node = node });
        }
    }
}
