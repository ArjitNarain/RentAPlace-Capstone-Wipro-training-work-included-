using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp3.Sorting
{ //selection sort
    internal class BubbleSortExample
    {
        public void SelectionSort(int[] myarry)
        {
            int n = myarry.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (myarry[j] < myarry[minIndex])
                    {
                        minIndex = j;
                    }
                    if (minIndex != i)
                    {
                        int temp = myarry[i];
                        myarry[i] = myarry[minIndex];
                        myarry[minIndex] = temp;
                    }
                }

            }
            foreach (int b in myarry)
            {
                Console.WriteLine(b);
            }
        }
       //bubble sort






        public void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)

            {

                for (int j = 0; j < n - 1 - i; j++)

                {


                    if (arr[j] > arr[j + 1])
                    {

                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
            foreach (int a in arr)
            {
                Console.WriteLine(a);
            }
        }





                        
             
             
           
    }
}

