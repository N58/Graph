using Graph.Interface;
using Graph.Logic;
using MoreLinq;
using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Graph.Algorithms
{
    class DijkstraAlgorithm : Algorithm
    {
        public DijkstraAlgorithm() : base() { }

        internal override void Execute()
        {
            while (Data.Nodes.List.Any(n => n.Status != Status.Visited))
            {
                Node current = Data.Nodes.List.FirstOrDefault(n => n.Status != Status.Visited);
                Data.Nodes.List.ForEach(x =>
                {
                    if (current.Distance > x.Distance && x.Status != Status.Visited)
                        current = x;
                });
                current.SetStatusWithDelay(Status.Current);

                List<Node> neighbours = Data.Connections.GetNeighbours(current);
                neighbours.ForEach(neighbour => 
                {
                    double newDistance = current.Distance + Data.Connections.GetConnection(current, neighbour).Value;
                    if (newDistance < neighbour.Distance)
                    {
                        neighbour.SetDistance(newDistance);
                        neighbour.Previous = current;
                    }
                });

                current.SetStatusWithDelay(Status.Visited);
            }
        }
    }
}
