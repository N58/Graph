using Graph.Logic;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;
using static Graph.Modes.AddOnCanvas;
using static Graph.Modes.ConnectOnCanvas;
using Graph.Graphics;
using Graph.Windows;

namespace Graph.Graphics
{
    static class DisplayOnCanvas
    {
        internal static void Initialize()
        {
            OnNodeAdded += NodeAdded;
            OnNodeConnecting += NodeConnecting;
            OnNodeConnected += NodeConnected;
            OnNodeConnectingFail += NodeConnectingFail;
        }

        private static Graphic FindGraphic(ILogicElement logicElement)
        {
            foreach (Graphic item in Data.UIElements.List)
            {
                if (item.LogicElement == logicElement)
                    return item;
            }

            return null;
        }

        private static void NodeAdded(object sender, OnNodeEventArgs e)
        {
            Ellipse circle = VisualConfig.AddCircle(e.Canvas, e.Node.Point, VisualConfig.CircleRadius, VisualConfig.CircleFill);
            string text = (Data.Nodes.List.IndexOf(e.Node) + 1).ToString();
            TextBlock textblock = VisualConfig.AddText(e.Canvas, e.Node.Point, text, Cursors.Hand);
            Graphic element = new Graphic(e.Node, circle, textblock);
            Data.UIElements.List.Add(element);
            e.Node.OnVisitedChanged += NodeVisitedChanged;
            e.Node.OnDistanceChanged += NodeDistanceChanged;
        }

        private static void NodeConnecting(object sender, OnNodeEventArgs e)
        {
            Graphic graphicNodeA = FindGraphic(e.Node);
            var circle = (Ellipse)graphicNodeA.Shape;
            circle.Fill = VisualConfig.CircleFillConnecting;
        }

        private static void NodeConnected(object sender, OnNodeConnectedEventArgs e)
        {
            Canvas canvas = e.canvas;
            Node A = e.connection.A;
            Node B = e.connection.B;
            double value = e.connection.value;
            Graphic graphicNodeA = FindGraphic(A);
            var circle = (Ellipse)graphicNodeA.Shape;

            circle.Fill = VisualConfig.CircleFill;


            Line line = VisualConfig.AddLine(canvas, A.Point, B.Point);

            double deltaX = A.X - B.X;
            double deltaY = A.Y - B.Y;
            double theta = (Math.Atan2(deltaY, deltaX) * (180 / Math.PI));

            if (theta >= 90 && theta <= 180)
                theta -= 180;
            else if (theta <= -90 && theta >= -180)
                theta += 180;

            double x = (A.X + B.X) / 2;
            double y = (A.Y + B.Y) / 2;
            Point middle = new Point(x, y);
            int move = 10;
            middle.X += move * Math.Cos((theta - 90) * (Math.PI / 180));
            middle.Y += move * Math.Sin((theta - 90) * (Math.PI / 180));

            TextBlock textblock = VisualConfig.AddText(canvas, middle, ((int)(value)).ToString(), theta);

            Graphic graphicObj = new Graphic(e.connection, line, textblock);
            Data.UIElements.List.Add(graphicObj);
        }

        private static void NodeConnectingFail(object sender, OnNodeEventArgs e)
        {
            Graphic graphicNodeA = FindGraphic(e.Node);
            var circle = (Ellipse)graphicNodeA.Shape;
            circle.Fill = VisualConfig.CircleFill;
        }

        private static void NodeVisitedChanged(object sender, EventArgs e)
        {
            var node = (Node)sender;
            var circle = (Ellipse)Data.UIElements.List.FirstOrDefault(n => n.LogicElement == node).Shape;
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                circle.Fill = node.GetStatusColorFill();
            }), DispatcherPriority.Background);
        }

        private static void NodeDistanceChanged(object sender, EventArgs e)
        {
            var node = (Node)sender;
        }

        public static void ModeChanged()
        {
            var win = (MainWindow)Application.Current.MainWindow;
            win.ModeText.Text = "Tryb - " + Data.Mode.ToString();
        }
    }
}
