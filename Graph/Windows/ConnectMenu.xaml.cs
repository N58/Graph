using Graph.Modes;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Graph.Windows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ConnectMenu : Window
    {
        public ConnectMenu()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Data.SetMode(ConnectingNodeMode.Instance);
            this.Close();
        }

        private void EditValue_Click(object sender, RoutedEventArgs e)
        {
            Data.SetMode(EditingConnectionMode.Instance);
            this.Close();
        }
    }
}
