using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Logic
{
    class Connections
    {
        public List<Connection> List { get; set; } = new List<Connection>();

        public bool ConnectionExist(Node a, Node b)
        {
            return GetConnection(a, b) != null;
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> result = new List<Node>();
            foreach (Connection conn in List)
            {
                if (conn.A == node)
                    result.Add(conn.B);
                else if (conn.B == node)
                    result.Add(conn.A);
            }

            return result;
        }

        public Connection GetConnection(Node a, Node b)
        {
            if (List != null)
                foreach (Connection conn in List.Where(conn => (conn.A == a && conn.B == b) || (conn.A == b && conn.B == a)))
                    return conn;

            return null;
        }
    }
}
