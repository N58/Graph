using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Graph
{
    class Graphic : UIElement
    {
        public ILogicElement LogicElement { get; set; }
        public UIElement Shape { get; set; }
        public UIElement Text { get; set; }

        public Graphic(ILogicElement logicElement, UIElement shape, UIElement text)
        {
            LogicElement = logicElement;
            Shape = shape;
            Text = text;
        }
    }
}
