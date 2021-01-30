using Graph.Interface;
using Graph.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Graph.Modes
{
    class ChoosingNodeMode : CanvasModes
    {
        public static ChoosingNodeMode Instance { get; } = new ChoosingNodeMode();

        public override void MouseDown(object sender, MouseButtonEventArgs e)
        {
            var canvas = (Canvas)sender;
            Point pos = Mouse.GetPosition(canvas);
            Node startingNode = Data.IsInAnyCircle(pos);
            if (startingNode != null)
            {
                Data.CurrentAlgorithm.StartingNode = startingNode;
                Data.CurrentAlgorithm.ResetValues();
                Data.CurrentAlgorithm.Run();
            }
        }

        public override string ToString()
        {
            Notification.SetInfo("Wybierz wierzchołek");
            return "Wybierania wierzchołka";
        }
    }
}
