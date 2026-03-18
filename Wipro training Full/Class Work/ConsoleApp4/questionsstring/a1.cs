using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.questionsstring
{
    internal class a1

    {
        static void Main(string[] args)
        {
            int[] numbers = { 10, 20, 30, 40, 50 };

            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                sum = sum + numbers[i];
            }
            double average = (double)sum / numbers.Length;
            {
                Console.WriteLine(average);
            }
        }

    }
}

