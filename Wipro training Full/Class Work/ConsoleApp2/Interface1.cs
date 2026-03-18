using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    internal interface Interface1
    {
        internal interface IBilling
        {
            void M1();
            void M2();
        }

        internal interface IGreeting
        {
            void M2();
            void M3();
        }

        internal partial class Sale : IBilling, IGreeting
        {
            void IGreeting.M2()
            {
                throw new NotImplementedException();
            }

            public void M3()
            {
                throw new NotImplementedException();
            }
        }

        internal partial class Sale : IGreeting
        {
            public void M1() { throw new NotImplementedException(); }
            public void M2() { }
        }
    }
}
