using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
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
    }
}
