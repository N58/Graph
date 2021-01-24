using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;
using static Graph.AddCanvasEvents;
using static Graph.ConnectCanvasEvents;

namespace Graph
{
    static class DisplayCanvasElements
    {
        // jakaś lista przechowująca wszystkie elementy graficzne z odniesieniem do ich logicznych obiektów
        static GraphicElements UIElements { get; set; } = new GraphicElements();

        internal static void Initialize()
        {
            OnNodeAdded += NodeAdded;
            OnNodeConnecting += NodeConnecting;
            OnNodeConnected += NodeConnected;
            OnNodeConnectingFail += NodeConnectingFail;
        }

        private static Graphic FindGraphic(ILogicElement logicElement)
        {
            foreach (Graphic item in UIElements.List)
            {
                if (item.LogicElement == logicElement)
                    return item;
            }

            return null;
        }

        private static void NodeAdded(object sender, OnNodeEventArgs e)
        {
            Ellipse circle = VisualData.AddCircle(e.canvas, e.node.Point, VisualData.CircleRadius, VisualData.CircleFill);
            string text = (Data.Nodes.List.IndexOf(e.node) + 1).ToString();
            TextBlock textblock = VisualData.AddText(e.canvas, e.node.Point, text, Cursors.Hand);
            Graphic element = new Graphic(e.node, circle, textblock);
            UIElements.List.Add(element);
            e.node.OnVisitedChanged += NodeVisitedChanged;
            e.node.OnDistanceChanged += NodeDistanceChanged;
        }

        private static void NodeConnecting(object sender, OnNodeEventArgs e)
        {
            Graphic graphicNodeA = FindGraphic(e.node);
            Ellipse circle = (Ellipse)graphicNodeA.Shape;
            circle.Fill = VisualData.CircleFillConnecting;
        }

        private static void NodeConnected(object sender, OnNodeConnectedEventArgs e)
        {
            Canvas canvas = e.canvas;
            Node A = e.connection.A;
            Node B = e.connection.B;
            double value = e.connection.value;
            Graphic graphicNodeA = FindGraphic(A);
            Ellipse circle = (Ellipse)graphicNodeA.Shape;

            circle.Fill = VisualData.CircleFill;


            Line line = VisualData.AddLine(canvas, A.Point, B.Point);

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

            TextBlock textblock = VisualData.AddText(canvas, middle, ((int)(value)).ToString(), theta);

            Graphic graphicObj = new Graphic(e.connection, line, textblock);
            UIElements.List.Add(graphicObj);
        }

        private static void NodeConnectingFail(object sender, OnNodeEventArgs e)
        {
            Graphic graphicNodeA = FindGraphic(e.node);
            Ellipse circle = (Ellipse)graphicNodeA.Shape;
            circle.Fill = VisualData.CircleFill;
        }

        private static void NodeVisitedChanged(object sender, EventArgs e)
        {
            Node node = (Node)sender;
            Ellipse circle = (Ellipse)UIElements.List.FirstOrDefault(n => n.LogicElement == node).Shape;
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (node.Status == Status.Standard)
                    circle.Fill = VisualData.CircleFill;
                else if (node.Status == Status.Current)
                    circle.Fill = VisualData.CircleFillCurrent;
                else if (node.Status == Status.Visited)
                    circle.Fill = VisualData.CircleFillVisited;
            }), DispatcherPriority.Background);
        }

        private static void NodeDistanceChanged(object sender, EventArgs e)
        {
            Node node = (Node)sender;
        }
    }
}
