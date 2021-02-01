using Graph.Logic;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;
using static Graph.Modes.AddingNodeModes;
using static Graph.Modes.ConnectingNodeMode;
using Graph.Graphics;
using Graph.Windows;
using System.Windows.Media;
using System.Threading.Tasks;

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
            Canvas canvas = e.Canvas;
            Ellipse circle = VisualConfig.SetCircle(e.Node.Point, VisualConfig.CircleRadius, VisualConfig.CircleFill);
            canvas.Children.Add(circle);
            string text = e.Node.Value.ToString();
            TextBlock textblock = VisualConfig.SetText(e.Node.Point, text, Cursors.Hand);
            canvas.Children.Add(textblock);
            Graphic element = new Graphic(e.Node, circle, textblock);
            Data.UIElements.List.Add(element);
            e.Node.OnVisitedChange += NodeVisitedChanged;
            e.Node.OnDistanceChange += NodeDistanceChanged;
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
            double value = e.connection.Value;
            Graphic graphicNodeA = FindGraphic(A);
            var circle = (Ellipse)graphicNodeA.Shape;

            circle.Fill = VisualConfig.CircleFill;


            Line line = VisualConfig.SetLine(A.Point, B.Point);
            canvas.Children.Add(line);

            double theta = CalcualteTextRotation(A, B);
            Point middle = CalculateTextPosition(A, B, theta);

            TextBlock textblock = VisualConfig.SetText(middle, ((int)(value)).ToString(), theta);
            canvas.Children.Add(textblock);

            Graphic graphicObj = new Graphic(e.connection, line, textblock);
            Data.UIElements.List.Add(graphicObj);
            e.connection.OnConnectionValueChange += ConnectionValueChanged;
        }        
        private static double CalcualteTextRotation(Node A, Node B)
        {
            double deltaX = A.X - B.X;
            double deltaY = A.Y - B.Y;
            double theta = (Math.Atan2(deltaY, deltaX) * (180 / Math.PI));

            if (theta >= 90 && theta <= 180)
                theta -= 180;
            else if (theta <= -90 && theta >= -180)
                theta += 180;

            return theta;
        }

        private static Point CalculateTextPosition(Node A, Node B, double theta = 0)
        {
            double x = (A.X + B.X) / 2;
            double y = (A.Y + B.Y) / 2;
            Point middle = new Point(x, y);
            int move = 10;
            middle.X += move * Math.Cos((theta - 90) * (Math.PI / 180));
            middle.Y += move * Math.Sin((theta - 90) * (Math.PI / 180));

            return middle;
        }

        private static void NodeConnectingFail(object sender, OnNodeEventArgs e)
        {
            Graphic graphicNodeA = FindGraphic(e.Node);
            var circle = (Ellipse)graphicNodeA.Shape;
            circle.Fill = VisualConfig.CircleFill;
        }

        private static void NodeVisitedChanged(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                var node = (Node)sender;
                var circle = (Ellipse)Data.UIElements.List.FirstOrDefault(n => n.LogicElement == node).Shape;
                circle.Fill = node.GetStatusColorFill();
            }), DispatcherPriority.Background);
        }

        private static void NodeDistanceChanged(object sender, EventArgs e)
        {
            var node = (Node)sender;
        }

        private static void ConnectionValueChanged(object sender, EventArgs e)
        {
            var conn = (Connection)sender;
            Graphic graphic = Data.UIElements.List.FirstOrDefault(c => c.LogicElement == conn);
            var oldTextblock = graphic.Text;
            var text = conn.Value.ToString();

            double theta = CalcualteTextRotation(conn.A, conn.B);
            Point middle = CalculateTextPosition(conn.A, conn.B, theta);
            var newTextblock = VisualConfig.SetText(middle, text, theta);

            graphic.Text = newTextblock;
            UpdateCanvas(oldTextblock, newTextblock);
        }

        private static void UpdateCanvas(UIElement oldUI, UIElement newUI)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            window.canvas.Children.Remove(oldUI);
            window.canvas.Children.Add(newUI);
        }

        public static void ModeChanged()
        {
            Data.ResetStatuses();
            var win = (MainWindow)Application.Current.MainWindow;
            win.ModeText.Text = "Tryb - " + Data.Mode.ToString();
        }
    }
}
