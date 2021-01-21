using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CanvasEvents events = AddCanvasEvents.Instance;

        public MainWindow()
        {
            InitializeComponent();
            DisplayCanvasElements.Initialize();
        }

        private void Refresh()
        {
            canvas.Children.Clear();

            foreach (Connection connection in Data.Connections)
            {
                VisualData.AddLine(canvas, connection.A.Point, connection.B.Point);

                double deltaX = connection.A.X - connection.B.X;
                double deltaY = connection.A.Y - connection.B.Y;
                double theta = (Math.Atan2(deltaY, deltaX) * (180 / Math.PI));
                //Trace.WriteLine(theta);
                if (theta > 90 && theta < 180)
                    theta -= 180;
                else if (theta < -90 && theta > -180)
                    theta += 180;

                double x = (connection.A.X + connection.B.X) / 2;
                double y = (connection.A.Y + connection.B.Y) / 2;
                Point middle = new Point(x, y);
                int move = 10;
                middle.X += move * Math.Cos((theta - 90) * (Math.PI / 180));
                middle.Y += move * Math.Sin((theta - 90) * (Math.PI / 180));

                VisualData.AddText(canvas, middle, ((int)(connection.value)).ToString(), theta);
            }

            foreach (Node node in Data.Nodes)
            {
                if (ConnectCanvasEvents.Connecting && node == ConnectCanvasEvents.A)
                    VisualData.AddCircle(canvas, node.Point, VisualData.CircleRadius, VisualData.CircleFillConnecting);
                else
                    VisualData.AddCircle(canvas, node.Point, VisualData.CircleRadius, VisualData.CircleFill);

                string text = (Data.Nodes.IndexOf(node) + 1).ToString();
                VisualData.AddText(canvas, node.Point, text, Cursors.Hand);
            }                
        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            events = AddCanvasEvents.Instance;
        }

        private void connect_button_Click(object sender, RoutedEventArgs e)
        {
            events = ConnectCanvasEvents.Instance;
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            events.MouseDown(sender, e);
        }
        
    }
}
