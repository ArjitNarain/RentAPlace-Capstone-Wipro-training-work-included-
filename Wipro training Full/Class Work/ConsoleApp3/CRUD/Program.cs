//using ConsoleApp3.CRUD;
//using ConsoleApp3.Models;
//using System;
//using System.Collections.Generic;

//namespace ConsoleApp3
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            bool check = true;

//            while (check)
//            {
//                Console.WriteLine("1. Add");
//                Console.WriteLine("2. Update");
//                Console.WriteLine("3. Delete");
//                Console.WriteLine("4. Fetch");
//                Console.WriteLine("5. Exit");
//                Console.WriteLine("--------------------------------------------------------");

//                Console.Write("Please choose your option: ");

//                int choice;
//                if (!int.TryParse(Console.ReadLine(), out choice))
//                {
//                    Console.WriteLine("Invalid input! Please enter a number.");
//                    continue;
//                }

//                Product p = new Product();
//                ProductCrud pc = new ProductCrud();

//                switch (choice)
//                {
//                    case 1:
//                        Console.Write("Please enter Product Name: ");
//                        p.Name = Console.ReadLine();

//                        Console.Write("Please enter Category Id: ");
//                        p.CategoryId = int.Parse(Console.ReadLine());

//                        Console.WriteLine(pc.AddProduct(p));
//                        break;

//                    case 2:
//                        Console.WriteLine("Enter Product Id to update:");
//                        p.ProductId = int.Parse(Console.ReadLine());

//                        Console.WriteLine("Enter Product name:");
//                        p.Name = Console.ReadLine();

//                        Console.WriteLine("Enter CategoryId:");
//                        p.CategoryId = int.Parse(Console.ReadLine());

//                        Product updatedProduct = pc.UpdateProduct(p);

//                        Console.WriteLine($"Updated -> ID: {updatedProduct.ProductId} | Name: {updatedProduct.Name} | CategoryId: {updatedProduct.CategoryId}");
//                        break;

//                    case 3:
//                        Console.WriteLine("Enter Product Id to delete:");
//                        p.ProductId = int.Parse(Console.ReadLine());

//                        Console.WriteLine(pc.DeleteProduct(p));
//                        break;

//                    case 4:
//                        List<Product> plist = pc.GetProducts();

//                        foreach (Product pr in plist)
//                        {
//                            Console.WriteLine($"ID: {pr.ProductId} | Name: {pr.Name} | CategoryId: {pr.CategoryId}");
//                        }
//                        break;

//                    case 5:
//                        check = false;
//                        break;

//                    default:
//                        Console.WriteLine("Invalid option selected.");
//                        break;
//                }

//                Console.WriteLine();
//            }
//        }
//    }
//}
