using Graph.Graphics;
using Graph.Logic;
using Graph.Windows;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Graph.Modes
{
    class EditOnCanvas : CanvasEvents
    {
        public static EditOnCanvas Instance { get; } = new EditOnCanvas();

        public override void MouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = (UIElement)e.Source;
            Graphic graphic = Data.UIElements.FindByTextOrShape(element);
            if (graphic == null)
                return;
            if (!(graphic.LogicElement is Connection))
                return;
            var window = new EditingMenu();
            window.Show();
        }

        public override string ToString()
        {
            return "Edytowania Połączeń";
        }
    }
}
