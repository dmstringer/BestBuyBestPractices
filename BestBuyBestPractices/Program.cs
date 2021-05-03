using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BestBuyBestPractices
{
    class Program
    {
        private static void PrintDepartment(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"Id: {depo.DepartmentID} Name: {depo.Name}");
            }
        }
        private static void PrintProduct(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine($"Id: {product.ProductID} Name: {product.Name} Price: {product.Price} CatagoryID: {product.CategoryID} On Sale: {product.OnSale} Stock Level: {product.StockLevel}");
            }
        }

        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion

            IDbConnection conn = new MySqlConnection(connString);
            DapperDepartmentRepository depRepo = new DapperDepartmentRepository(conn);
            DapperProductRepository prodRepo = new DapperProductRepository(conn);

            Console.WriteLine("Hello user, here are the current departments:");
            Console.WriteLine("Please press enter . . .");
            Console.ReadLine();

            var depos = depRepo.GetAllDepartments();
            PrintDepartment(depos);

            Console.WriteLine("Do you want to add a department?");
            string userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of your new Department?");
                userResponse = Console.ReadLine();
                depRepo.InsertDepartment(userResponse);
                PrintDepartment(depRepo.GetAllDepartments());
            }

            Console.WriteLine();
            Console.WriteLine("Now here are the current products:");
            Console.WriteLine("Please press enter . . .");
            Console.ReadLine();

            var products = prodRepo.GetAllProducts();
            PrintProduct(products);

            Console.WriteLine("Do you want to add a product?");
            string userResponse2 = Console.ReadLine();

            if (userResponse2.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of your new Product?");
                string userResponse3 = Console.ReadLine();
                Console.WriteLine("What is the price of your new Product?");
                string userResponse4 = Console.ReadLine();
                Console.WriteLine("What is the category ID of your new Product?");
                string userResponse5 = Console.ReadLine();
                prodRepo.CreateProduct(userResponse3, Double.Parse(userResponse4), Int32.Parse(userResponse5));
                PrintProduct(prodRepo.GetAllProducts());
            }
        }

       
    }
}
