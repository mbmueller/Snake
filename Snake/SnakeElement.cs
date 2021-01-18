using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class SnakeElement : Element
    {
        private Square inhalt { get; }
        private Element next;

        public SnakeElement(Square d)
        {
            inhalt = d;
            next = new Abschluss();
        }
        SnakeElement()
        {
            next = new Abschluss();
        }

        public override Element Add(Element d)
        {
            next = next.Add(d);
            return this;
        }

        public override int Count()
        {
            return next.Count() + 1;
        }

        public override Square GetSquare(int index, int v)
        {
            if (index == v)
                return inhalt;

            return next.GetSquare(index, v + 1);
        }
    }
}
