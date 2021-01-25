using Graph.Logic;
using MoreLinq;
using MoreLinq.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Algorithms
{
    class DijkstraAlgorithm : Algorithm
    {
        public DijkstraAlgorithm(Node startingNode, List<Node> nodes, List<Connection> connections)
        {
            DataForAlgorithm.Nodes.List = nodes;
            DataForAlgorithm.Nodes.List.ForEach(n => n.SetDistance(double.PositiveInfinity));
            startingNode.SetDistance(0);
            DataForAlgorithm.Connections.List = connections;
            Task.Factory.StartNew(() => Run());
        }

        void Run()
        {
            while(DataForAlgorithm.Nodes.List.Any(n => n.Status != Status.Visited))
            {
                Node current = DataForAlgorithm.Nodes.List.FirstOrDefault(n => n.Status != Status.Visited);
                DataForAlgorithm.Nodes.List.ForEach(x =>
                {
                    if (current.Distance > x.Distance && x.Status != Status.Visited)
                        current = x;
                });
                current.SetStatus(Status.Current);
                Thread.Sleep(1000);

                List<Node> neighbours = DataForAlgorithm.Connections.GetNeighbours(current);
                foreach (Node neighbour in neighbours)
                {
                    double newDistance = current.Distance + DataForAlgorithm.Connections.GetConnection(current, neighbour).value;
                    if (newDistance < neighbour.Distance)
                    {
                        neighbour.SetDistance(newDistance);
                        neighbour.Previous = current;
                    }
                }
                current.SetStatus(Status.Visited);
                Thread.Sleep(1000);
            }
        }
    }
}
