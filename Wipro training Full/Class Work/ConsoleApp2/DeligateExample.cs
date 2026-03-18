//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ConsoleApp2
//{
//    internal class DeligateExample
//    {

//        static void Main(string[] args)
//        {
//            DeligateExample de = new DeligateExample();
//            Action<int> al = de.Cube;
//            al += de.Quad;
//            var invlist = al.GetInvocationList().Cast<Action<int>>();
//            foreach(var a in invlist)
//            {
//                a.Invoke(10);

//            }
//        }


//        public class DeligateExample
//        {
            
//                public int Double(int x)
//                {
//                    return x + x;
//                }
//                public int Square(int x)
//                {
//                    return x * x;
//                }

//                public void Cube(int x)
//            {
//                Console.WriteLine(x * x * x);
//            }

//            public void Quad(int x)
//            {
//                Console.WriteLine(x * x * x * x);
//            }



//        }
//        }
//    }
//}
