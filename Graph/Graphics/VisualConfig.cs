using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Graph.Graphics
{
    static class VisualConfig
    {
        // Circle
        public static double CircleRadius { get; } = 40;
        public static Brush CircleFill { get; } = new SolidColorBrush(Color.FromRgb(120, 120, 255));
        public static Brush CircleFillCurrent { get; } = new SolidColorBrush(Color.FromRgb(255, 50, 50));
        public static Brush CircleFillVisited { get; } = new SolidColorBrush(Color.FromRgb(150, 150, 150));
        public static double CircleStrokeThickness { get; } = 0.5;
        public static Brush CircleStroke { get; } = Brushes.Black;
        public static Brush CircleFillConnecting { get; } = new SolidColorBrush(Color.FromRgb(0, 255, 0));

        // Line
        public static double LineStrokeThickness { get; } = 2;
        public static Brush LineStroke { get; } = Brushes.Black;

        // Text
        public static double TextFontSize { get; } = 18;
        public static string TextFontFamily { get; } = "arial";
        public static double TextRotation { get; } = 0;
        public static Brush TextColor { get; } = Brushes.Black;
        public static Cursor TextCursor { get; } = Cursors.Arrow;

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
        public static TextBlock AddText(Canvas canvas, Point position, string text, Cursor cursor)
        {
            return AddText(canvas, position, text, TextColor, cursor, TextFontSize, TextFontFamily, TextRotation);
        }
    }
}
