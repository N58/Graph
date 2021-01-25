﻿using Graph.Graphics;
using Graph.Logic;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Graph.Modes
{
    class AddOnCanvas : CanvasEvents
    {
        public static AddOnCanvas Instance { get; } = new AddOnCanvas();

        public static event EventHandler<OnNodeEventArgs> OnNodeAdded;
        public class OnNodeEventArgs : EventArgs
        {
            public Canvas Canvas { get; set; }
            public Node Node { get; set; }
        }

        public override void MouseDown(object sender, MouseButtonEventArgs e)
        {
            var canvas = (Canvas)sender;
            Point pos = Mouse.GetPosition(canvas);
            var node = new Node(pos);

            if (pos.X + (VisualConfig.CircleRadius / 2) > canvas.Width ||
                pos.X - (VisualConfig.CircleRadius / 2) < 0 ||
                pos.Y + (VisualConfig.CircleRadius / 2) > canvas.Height ||
                pos.Y - (VisualConfig.CircleRadius / 2) < 0)
                return;

            if (Data.Nodes.List.Any(n => Data.IsInCircle(pos, n))) return;

            Data.Nodes.List.Add(node);
            OnNodeAdded?.Invoke(this, new OnNodeEventArgs { Canvas = canvas, Node = node });
        }

        public override string ToString()
        {
            return "Dodawania wierzchołka";
        }
    }
}
