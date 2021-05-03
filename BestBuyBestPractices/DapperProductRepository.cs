using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBuyBestPractices
{
    class DapperProductRepository : IProductRepository
    {
        //Field or local Variable for making queries to the DB
        private readonly IDbConnection _connection;

        //Constructor
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _connection.Query<Product>("SELECT * FROM products").ToList();
            return products;
        }

        public void CreateProduct(string newName, double newPrice, int newCategoryID)
        {
            _connection.Execute("INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (@name, @price, @categoryId);",
            new { name = newName, price = newPrice, categoryId = newCategoryID });
        }
    }
}
