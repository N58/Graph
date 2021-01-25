using Graph.Algorithms;
using Graph.Graphics;
using Graph.Modes;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Graph.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Application.Current.MainWindow = this;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            InitializeComponent();
            DisplayOnCanvas.Initialize();
            Data.SetMode(AddOnCanvas.Instance);
        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            Data.SetMode(AddOnCanvas.Instance);
        }

        private void connect_button_Click(object sender, RoutedEventArgs e)
        {
            var subWindow = new ConnectMenu();
            subWindow.Show();
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Data.Mode.MouseDown(sender, e);
        }

        private void algorithm_button_Click(object sender, RoutedEventArgs e)
        {
            DijkstraAlgorithm dijkstra = new DijkstraAlgorithm(Data.Nodes.List[0], Data.Nodes.List, Data.Connections.List);
        }
    }
}
