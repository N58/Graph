using Graph.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Graph.Modes.AddingNodeModes;

namespace Graph.Modes
{
    class ConnectingNodeMode : CanvasModes
    {
        public static ConnectingNodeMode Instance { get; } = new ConnectingNodeMode();
        public static bool Connecting = false;
        public static Node NodeA;
        public static Node NodeB;

        public static event EventHandler<OnNodeEventArgs> OnNodeConnecting;
        public static event EventHandler<OnNodeConnectedEventArgs> OnNodeConnected;
        public static event EventHandler<OnNodeEventArgs> OnNodeConnectingFail;
        public class OnNodeConnectedEventArgs : EventArgs
        {
            public Canvas canvas;
            public Connection connection;
        }

        public override void MouseDown(object sender, MouseButtonEventArgs e)
        {
            var canvas = (Canvas)sender;
            Point pos = Mouse.GetPosition(canvas);

            Node curr = Data.IsInAnyCircle(pos);

            if (curr == null && NodeA != null)
            {
                ConnectionFailed(canvas);
                return;
            }

            if (curr == null)
                return;

            if (!Connecting)
            {
                NodeA = curr;
                Connecting = true;
                OnNodeConnecting?.Invoke(this, new OnNodeEventArgs { Canvas = canvas, Node = NodeA });
            }
            else
            {
                NodeB = curr;
                var conn = new Connection(NodeA, NodeB);

                if (curr == conn.A || Data.Connections.ConnectionExist(conn.A, conn.B))
                {
                    ConnectionFailed(canvas);
                    return;
                }

                Data.Connections.List.Add(conn);
                Connecting = false;
                OnNodeConnected?.Invoke(this, new OnNodeConnectedEventArgs { canvas = canvas, connection = conn });
            }
        }

        private void ConnectionFailed(Canvas canvas)
        {
            OnNodeConnectingFail?.Invoke(this, new OnNodeEventArgs { Canvas = canvas, Node = NodeA });
            NodeA = null;
            NodeB = null;
            Connecting = false;
            return;
        }

        public override string ToString()
        {
            return "Łączenia wierzchołków";
        }
    }
}
