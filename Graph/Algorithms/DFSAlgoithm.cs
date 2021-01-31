using Graph.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Algorithms
{
    class DFSAlgoithm : Algorithm
    {
        internal override void Execute()
        {
            Data.Nodes.List.ForEach(n => 
            {
                if (n.Status != Status.Visited)
                    DFS(n);
            });
        }

        private void DFS(Node current)
        {
            current.SetStatusWithDelay(Status.Current);
            List<Node> neighbours = Data.Connections.GetNeighbours(current);
            current.SetStatus(Status.Visited);
            neighbours.ForEach(neighbour =>
            {
                if (neighbour.Status != Status.Visited)
                {
                    DFS(neighbour);
                }
            });
        }
    }
}
