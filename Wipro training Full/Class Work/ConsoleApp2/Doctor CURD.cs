using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    internal class Doctor_CURD
    {
        public static Doctor AddDoctor()
        {
            Doctor d = new Doctor();
            Console.Write("Please enter doctor Id: ");
            d.Id=int.Parse(Console.ReadLine());

            Console.Write("Please enter doctor Name: ");
            d.Name = Console.ReadLine();
            Console.Write("Please enter Specialization :");
            d.Specialization = Console.ReadLine();
            return d;
        }
    }
}
