using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using static Graph.AddCanvasEvents;
using static Graph.ConnectCanvasEvents;

namespace Graph
{
    static class DisplayCanvasElements
    {
        // jakaś lista przechowująca wszystkie elementy graficzne z odniesieniem do ich logicznych obiektów
        static List<GraphicObject> UIElements = new List<GraphicObject>();

        internal static void Initialize()
        {
            OnNodeAdded += NodeAdded;
            OnNodeConnecting += NodeConnecting;
            OnNodeConnected += NodeConnected;
            OnNodeConnectingFail += NodeConnectingFail;
        }

        private static GraphicObject FindGraphic(ILogicElement logicElement)
        {
            foreach (GraphicObject item in UIElements)
            {
                if (item.LogicElement == logicElement)
                    return item;
            }

            return null;
        }

        private static void NodeAdded(object sender, OnNodeEventArgs e)
        {
            Ellipse circle = VisualData.AddCircle(e.canvas, e.node.Point, VisualData.CircleRadius, VisualData.CircleFill);
            string text = (Data.Nodes.IndexOf(e.node) + 1).ToString();
            TextBlock textblock = VisualData.AddText(e.canvas, e.node.Point, text, Cursors.Hand);
            GraphicObject element = new GraphicObject(e.node, circle, textblock);
            UIElements.Add(element);
        }

        private static void NodeConnecting(object sender, OnNodeEventArgs e)
        {
            GraphicObject graphicNodeA = FindGraphic(e.node);
            Ellipse circle = (Ellipse)graphicNodeA.Shape;
            circle.Fill = VisualData.CircleFillConnecting;
        }

        private static void NodeConnected(object sender, OnNodeConnectedEventArgs e)
        {
            Canvas canvas = e.canvas;
            Node A = e.connection.A;
            Node B = e.connection.B;
            double value = e.connection.value;
            GraphicObject graphicNodeA = FindGraphic(A);
            Ellipse circle = (Ellipse)graphicNodeA.Shape;

            circle.Fill = VisualData.CircleFill;


            Line line = VisualData.AddLine(canvas, A.Point, B.Point);

            double deltaX = A.X - B.X;
            double deltaY = A.Y - B.Y;
            double theta = (Math.Atan2(deltaY, deltaX) * (180 / Math.PI));

            if (theta > 90 && theta < 180)
                theta -= 180;
            else if (theta < -90 && theta > -180)
                theta += 180;

            double x = (A.X + B.X) / 2;
            double y = (A.Y + B.Y) / 2;
            Point middle = new Point(x, y);
            int move = 10;
            middle.X += move * Math.Cos((theta - 90) * (Math.PI / 180));
            middle.Y += move * Math.Sin((theta - 90) * (Math.PI / 180));

            TextBlock textblock = VisualData.AddText(canvas, middle, ((int)(value)).ToString(), theta);

            GraphicObject graphicObj = new GraphicObject(e.connection, line, textblock);
            UIElements.Add(graphicObj);
        }

        private static void NodeConnectingFail(object sender, OnNodeEventArgs e)
        {
            GraphicObject graphicNodeA = FindGraphic(e.node);
            Ellipse circle = (Ellipse)graphicNodeA.Shape;
            circle.Fill = VisualData.CircleFill;
        }
    }
}
