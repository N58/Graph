using Graph.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Algorithms
{
    class BFSAlgorithm : Algorithm
    {
        internal override void Execute()
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(StartingNode);
            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                current.SetStatusWithDelay(Status.Current);

                List<Node> neighbours = Data.Connections.GetNeighbours(current);
                neighbours.ForEach(neighbour =>
                {
                    if(neighbour.Status != Status.Visited && !queue.Contains(neighbour))
                    {
                        queue.Enqueue(neighbour);
                    }
                });
                current.SetStatusWithDelay(Status.Visited);
            }
        }
    }
}
