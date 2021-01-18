using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Abschluss : Element
    {
        public override Element Add(Element d)
        {
            return d;
        }

        public override int Count()
        {
            return 0;
        }

        public override Square GetSquare(int index, int v)
        {
            return null;
        }
    }
}
