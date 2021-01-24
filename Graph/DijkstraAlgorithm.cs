using MoreLinq;
using MoreLinq.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Graph
{
    class DijkstraAlgorithm : Algorithm
    {
        public DijkstraAlgorithm(Node startingNode, List<Node> nodes, List<Connection> connections)
        {
            Datas.Nodes.List = nodes;
            Datas.Nodes.List.ForEach(n => n.SetDistance(double.PositiveInfinity));
            startingNode.SetDistance(0);
            Datas.Connections.List = connections;
            Task.Factory.StartNew(() => Run());
        }

        void Run()
        {
            while(Datas.Nodes.List.Any(n => n.Status != Status.Visited))
            {
                Node current = Datas.Nodes.List.FirstOrDefault(n => n.Status != Status.Visited);
                Datas.Nodes.List.ForEach(x =>
                {
                    if (current.Distance > x.Distance && x.Status != Status.Visited)
                        current = x;
                });
                current.SetStatus(Status.Current);

                List<Node> neighbours = Datas.Connections.GetNeighbours(current);
                foreach (Node neighbour in neighbours)
                {
                    double newDistance = current.Distance + Datas.Connections.GetConnection(current, neighbour).value;
                    if (newDistance < neighbour.Distance)
                    {
                        neighbour.SetDistance(newDistance);
                        neighbour.Previous = current;
                    }
                }
                current.SetStatus(Status.Visited);
            }
        }
    }
}
