using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Graph.AddCanvasEvents;

namespace Graph
{
    class ConnectCanvasEvents : CanvasEvents
    {
        private static readonly ConnectCanvasEvents instance = new ConnectCanvasEvents();
        public static bool Connecting = false;
        public static Node A;
        public static Node B;

        public static ConnectCanvasEvents Instance
        {
            get
            {
                return instance;
            }
        }

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
            Canvas canvas = (Canvas)sender;
            Point pos = Mouse.GetPosition(canvas);

            Node curr = Data.IsInAnyCircle(pos);

            if (curr == null)
            {
                ConnectionFailed(canvas);
                return;
            }

            if (!Connecting)
            {
                A = curr;
                Connecting = true;
                OnNodeConnecting?.Invoke(this, new OnNodeEventArgs { canvas = canvas, node = A });
            }
            else
            {
                B = curr;
                Connection conn = new Connection(A, B);

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
            OnNodeConnectingFail?.Invoke(this, new OnNodeEventArgs { canvas = canvas, node = A });
            A = null;
            B = null;
            Connecting = false;
            return;
        }
    }
}
