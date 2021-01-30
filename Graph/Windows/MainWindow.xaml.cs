using Graph.Graphics;
using Graph.Modes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Graph.Windows
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Application.Current.MainWindow = this;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            InitializeComponent();
            DisplayOnCanvas.Initialize();
            Data.SetMode(AddingNodeModes.Instance);
        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            Data.SetMode(AddingNodeModes.Instance);
        }

        private void connect_button_Click(object sender, RoutedEventArgs e)
        {
            var subWindow = new ConnectMenu();
            subWindow.ShowDialog();
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Data.Mode.MouseDown(sender, e);
        }

        private void algorithm_button_Click(object sender, RoutedEventArgs e)
        {
            var subWindow = new AlgorithmsMenu();
            subWindow.ShowDialog();
        }
    }
}
