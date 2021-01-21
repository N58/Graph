using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Graph
{
    static class VisualData
    {
        // Circle
        private static double circleRadius = 40;
        private static Brush circleFill = new SolidColorBrush(Color.FromRgb(120, 120, 255));
        private static double circleStrokeThickness = 0.5;
        private static Brush circleStroke = Brushes.Black;
        private static Brush circleFillConnecting = new SolidColorBrush(Color.FromRgb(0, 255, 0));

        public static double CircleRadius { get { return circleRadius; } }
        public static Brush CircleFill { get { return circleFill; } }
        public static double CircleStrokeThickness { get { return circleStrokeThickness; } }
        public static Brush CircleStroke { get { return circleStroke; } }
        public static Brush CircleFillConnecting { get { return circleFillConnecting; } }

        // Line
        private static double lineStrokeThickness = 2;
        private static Brush lineStroke = Brushes.Black;

        public static double LineStrokeThickness { get { return lineStrokeThickness; } }
        public static Brush LineStroke { get { return lineStroke; } }

        // Text
        private static double textFontSize = 18;
        private static string textFontFamily = "arial";
        private static double textRotation = 0;
        private static Brush textColor = Brushes.Black;
        private static Cursor textCursor = Cursors.Arrow;

        public static double TextFontSize { get { return textFontSize; } }
        public static string TextFontFamily { get { return textFontFamily; } }
        public static double TextRotation { get { return textRotation; } }
        public static Brush TextColor { get { return textColor; } }
        public static Cursor TextCursor { get { return textCursor; } }

        public static Line AddLine(Canvas canvas, Point A, Point B)
        {
            Line line = new Line()
            {
                X1 = A.X,
                Y1 = A.Y,
                X2 = B.X,
                Y2 = B.Y,
                StrokeThickness = LineStrokeThickness,
                Stroke = LineStroke
            };

            Canvas.SetZIndex(line, -1);
            canvas.Children.Add(line);
            return line;
        }

        public static Ellipse AddCircle(Canvas canvas, Point position, double radius, Brush fill, Cursor cursor, double strokeThickness, Brush stroke)
        {
            Ellipse circle = new Ellipse()
            {
                Width = radius,
                Height = radius,
                Fill = fill,
                StrokeThickness = strokeThickness,
                Stroke = stroke,
                Cursor = cursor
            };

            Canvas.SetLeft(circle, position.X - (circle.Width / 2));
            Canvas.SetTop(circle, position.Y - (circle.Height / 2));

            canvas.Children.Add(circle);
            return circle;
        }
        public static Ellipse AddCircle(Canvas canvas, Point position, double radius, Brush fill, Cursor cursor)
        {
            return AddCircle(canvas, position, radius, fill, cursor, CircleStrokeThickness, CircleStroke);
        }
        public static Ellipse AddCircle(Canvas canvas, Point position, double radius, Brush fill)
        {
            return AddCircle(canvas, position, radius, fill, Cursors.Hand, CircleStrokeThickness, CircleStroke);
        }

        public static TextBlock AddText(Canvas canvas, Point position, string text, Brush color, Cursor cursor, double fontSize, string fontFamily, double rotation)
        {
            TextBlock textblock = new TextBlock()
            {
                Text = text,
                Foreground = color,
                FontSize = fontSize,
                FontFamily = new FontFamily(fontFamily),
                Cursor = cursor
            };

            RotateTransform rotate = new RotateTransform(rotation);
            textblock.RenderTransform = rotate;
            textblock.RenderTransformOrigin = new Point(0.5, 0.5);

            textblock.Arrange(new Rect(textblock.DesiredSize));

            Canvas.SetLeft(textblock, position.X - (textblock.ActualWidth / 2));
            Canvas.SetTop(textblock, position.Y - (textblock.ActualHeight / 2));

            canvas.Children.Add(textblock);

            return textblock;
        }
        public static TextBlock AddText(Canvas canvas, Point position, string text, double rotation)
        {
            return AddText(canvas, position, text, TextColor, TextCursor, TextFontSize, TextFontFamily, rotation);
        }
        public static TextBlock AddText(Canvas canvas, Point position, string text, Brush color)
        {
            return AddText(canvas, position, text, color, TextCursor, TextFontSize, TextFontFamily, TextRotation);
        }
        public static TextBlock AddText(Canvas canvas, Point position, string text, Cursor cursor)
        {
            return AddText(canvas, position, text, TextColor, cursor, TextFontSize, TextFontFamily, TextRotation);
        }
        public static TextBlock AddText(Canvas canvas, Point position, string text)
        {
            return AddText(canvas, position, text, TextColor, TextCursor, TextFontSize, TextFontFamily, TextRotation);
        }
    }
}
