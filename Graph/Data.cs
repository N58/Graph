using Graph.Algorithms;
using Graph.Graphics;
using Graph.Logic;
using Graph.Modes;
using System.Linq;
using System.Windows;

namespace Graph
{
    static class Data
    {
        public static CanvasModes Mode { get; private set; } = AddingNodeModes.Instance;

        public static GraphicElements UIElements { get; set; } = new GraphicElements();

        public static Nodes Nodes { get; set; } = new Nodes();

        public static Connections Connections { get; set; } = new Connections();
        public static Algorithm CurrentAlgorithm { get; set; } = null;

        public static bool IsInCircle(Point position, Node node)
        {
            double a = (position.X - node.X) * (position.X - node.X);
            double b = (position.Y - node.Y) * (position.Y - node.Y);
            double r = VisualConfig.CircleRadius;
            r *= r;

            return (a + b) < r;
        }

        public static Node IsInAnyCircle(Point position)
        {
            return Nodes.List.FirstOrDefault(n => IsInCircle(position, n));
        }

        public static void SetMode(CanvasModes mode)
        {
            Mode = mode;
            Mode.Initialize();
            DisplayOnCanvas.ModeChanged();
        }

        internal static void ResetStatuses()
        {
            Data.Nodes.List.ForEach(n => n.SetStatus(Status.Default));
        }
    }
}
