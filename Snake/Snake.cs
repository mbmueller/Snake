using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Snake
    {
        private Element head;
        public Snake()
        {
            head = new Abschluss();
        }

        public void Add(Square d)
        {
            head = head.Add(new SnakeElement(d));
        }

        public int Length()
        {
            return head.Count();
        }

        public Square GetSquare(int index)
        {
            return head.GetSquare(index, 0);
        }

        public void Clear()
        {
            head = new Abschluss();
        }
    }
}
