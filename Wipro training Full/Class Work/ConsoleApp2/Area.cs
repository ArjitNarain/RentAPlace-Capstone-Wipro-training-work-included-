using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    internal class Area
    {
        public int length;
        public int width=0;

        public Area(int l,int b)
        {
            length = l;
            width = b;
        }

        public Area(int l)
        {
            length = l;
            
        }
        public int CalculateArea()
        {
            if (width == 0)
            {
                return length * length;
            }
            else
            {
                return length * width;

            }
        }
    }
}
