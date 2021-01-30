using Graph.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Graph.Modes
{
    class ProcessingAlgorithmMode : CanvasModes
    {
        public static ProcessingAlgorithmMode Instance { get; } = new ProcessingAlgorithmMode();

        public override void Initialize()
        {
            ((MainWindow)Application.Current.MainWindow).InterfaceButtons.IsEnabled = false;
        }

        public override void MouseDown(object sender, MouseButtonEventArgs e)
        {
            return;
        }

        public override string ToString()
        {
            return "Działania algorytmu";
        }
    }
}
