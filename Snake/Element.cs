using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public abstract class Element
    {
        public Element()
        {
        }

        public abstract Element Add(Element d);
        public abstract int Count();

        public abstract Square GetSquare(int index, int v);
    }
}
