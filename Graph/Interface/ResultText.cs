using Graph.Algorithms;
using Graph.Windows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace Graph.Interface
{
    static class ResultText
    {
        private static MainWindow window = (MainWindow)Application.Current.MainWindow;

        public static void UpdateResult(Algorithm alg)
        {

            window.Result.Text = "Wynik: " + string.Join(",", alg.Result.Select(n => n.Value));
        }
    }
}
