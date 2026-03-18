using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp3.Sorting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] marks = { 90, 89, 87, 99, 100 };
            BubbleSortExample bse=new BubbleSortExample();
            bse.BubbleSort(marks);
            bse.SelectionSort(marks);

        }
    }
}
