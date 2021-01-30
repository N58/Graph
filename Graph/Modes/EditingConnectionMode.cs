using Graph.Graphics;
using Graph.Logic;
using Graph.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Graph.Modes
{
    class EditingConnectionMode : CanvasModes
    {
        public static EditingConnectionMode Instance { get; } = new EditingConnectionMode();
        private Graphic current;

        public override void MouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = (UIElement)e.Source;
            current = Data.UIElements.FindByTextOrShape(element);
            if (current == null)
                return;
            if (!(current.LogicElement is Connection))
                return;
            var window = new EditingMenu();
            window.Closed += OnEditWindowClose;
            window.ShowDialog();
        }

        private void OnEditWindowClose(object sender, EventArgs e)
        {
            var window = (EditingMenu)sender;
            string text = window.EditValue.Text;
            if (double.TryParse(text, out var value))
            {
                var conn = (Connection)current.LogicElement;
                conn.SetValue(value);
            }
        }

        public override string ToString()
        {
            return "Edytowania Połączeń";
        }
    }
}
