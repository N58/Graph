using Graph.Interface;
using Graph.Logic;
using Graph.Modes;
using Graph.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Graph.Algorithms
{
    abstract class Algorithm
    {
        public Algorithm()
        {
            Data.SetMode(ChoosingNodeMode.Instance);
            Data.CurrentAlgorithm = this;
        }
        internal Node StartingNode { get; set; }

        internal virtual void ResetValues()
        {
            Data.Nodes.List.ForEach(n => n.SetDistance(double.PositiveInfinity));
            StartingNode.SetDistance(0);
        }

        internal void Run()
        {
            Data.SetMode(ProcessingAlgorithmMode.Instance);
            Notification.SetInfo("Trwa wykonywanie algorytmu...");
            Task.Factory.StartNew(() => this.Execute()).ContinueWith(n => Finish());
        }

        internal abstract void Execute();

        internal void Finish()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                Notification.SetConfirmation("Zakończono wykonywanie algorytmu");
                Data.CurrentAlgorithm = null;
                ((MainWindow)Application.Current.MainWindow).InterfaceButtons.IsEnabled = true;
            }), DispatcherPriority.Background);
        }
    }
}
