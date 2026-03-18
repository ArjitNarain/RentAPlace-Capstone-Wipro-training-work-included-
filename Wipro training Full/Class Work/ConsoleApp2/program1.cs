using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    internal class program1
    {
        static void Main(string[] args)

        
           
    
        
            {
                DeligateExample de = new DeligateExample();
                Action<int> al = de.Cube;
                al += de.Quad;
                var invlist = al.GetInvocationList().Cast<Action<int>>();
                foreach (var a in invlist)
                {
                    a.Invoke(10);

                }
            }


            public class DeligateExample
            {

                public int Double(int x)
                {
                    return x + x;
                }
                public int Square(int x)
                {
                    return x * x;
                }

                public void Cube(int x)
                {
                    Console.WriteLine(x * x * x);
                }

                public void Quad(int x)
                {
                    Console.WriteLine(x * x * x * x);
                }



            }
    } 
}

//            Hashtable ht = new Hashtable();
//            ht.Add(1, "India");
//            ht.Add(2, "Japan");
//            foreach (DictionaryEntry de in ht)
//            {
//                Console.WriteLine(de.Key + " " + de.Value);


//            }

//            SortedList<int, string> sl = new SortedList<int, string>();
//            sl.Add(1, "Country");
//            sl.Add(3, "District");
//            sl.Add(2, "State");

//            foreach (KeyValuePair<int, string> kvp in sl)
//            {
//                Console.WriteLine(kvp.Key + " " + kvp.Value);
//            }

//        }
//    }
//}

//        {
//            List<Doctor> doctors = new List<Doctor>();
//            Doctor d = Doctor_CURD.AddDoctor();
//            doctors.Add(d);

//            foreach (Doctor doctor in doctors)
//            {
//                Console.WriteLine($"ID : {doctor.Id} | Name : {doctor.Name} | Specialization : {doctor.Specialization}");
//            }
//            //List<string> countries = new List<string>();
//            //countries.Add("India");
//            //countries.Add("USA");
//            //countries.Add("Japan");
//            //countries.Add("China");
//            //foreach (string country in countries)
//            //{
//            //    Console.WriteLine(country);

//            //    //Area a = new Area(100,50);
//            //    //Console.Write(a.CalculateArea());

//            //}
//            //countries.Remove("USA");
//            //countries.RemoveAt(1);
//            //countries.RemoveAll(e=>e.Equals("China"));

//            //foreach(string c in countries)
//            //{
//            //    Console.WriteLine(c); 
//            //}
//            ////this is list example

//            //Dictionary<int, string> dict = new Dictionary<int, string>();
//            //dict.Add(1, "India");
//            //dict.Add(2, "USA");
//            //dict.Add(3, "Japan");
//            //dict.Add(4, "China");

//            //foreach (KeyValuePair<int, string> kv in dict)
//            //{
//            //    Console.WriteLine(kv.Key + " " + kv.Value);
//            //}
//            //dict.Remove(1);
//            //dict.Clear();


//        } //this is dictnory example
//    }
//}
