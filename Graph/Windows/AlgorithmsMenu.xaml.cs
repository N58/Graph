using Graph.Algorithms;
using System.Threading.Tasks;
using System.Windows;

namespace Graph.Windows
{
    /// <summary>
    /// Interaction logic for AlgorithmsMenu.xaml
    /// </summary>
    public partial class AlgorithmsMenu : Window
    {

        public AlgorithmsMenu()
        {
            InitializeComponent();
        }

        private void Dijkstra_Click(object sender, RoutedEventArgs e)
        {
            var dijkstra = new DijkstraAlgorithm();
            this.Close();
        }

        private void DFS_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BFS_Click(object sender, RoutedEventArgs e)
        {
            var bfs = new BFSAlgorithm();
            this.Close();
        }
    }
}
