using Graph.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Graph.Graphics
{
    class GraphicElements
    {
        public List<Graphic> List { get; set; } = new List<Graphic>();

        public List<T> GetGraphicOfType<T>()
        {
            List<T> result = new List<T>();
            foreach (Graphic item in List.Where(n => n.LogicElement.GetType() == typeof(Node)))
            {
                T logic = (T)item.LogicElement;
                result.Add(logic);
            }

            return result;
        }

        public Graphic FindByLogic(ILogicElement logicElement)
        {
            return List.FirstOrDefault(n => n.LogicElement == logicElement);
        }

        public Graphic FindByTextOrShape(UIElement UI)
        {
            if (List.Any(n => n.Shape == UI))
                return List.FirstOrDefault(n => n.Shape == UI);
            else if (List.Any(n => n.Text == UI))
                return List.FirstOrDefault(n => n.Text == UI);
            else
                return null;
        }
    }
}
