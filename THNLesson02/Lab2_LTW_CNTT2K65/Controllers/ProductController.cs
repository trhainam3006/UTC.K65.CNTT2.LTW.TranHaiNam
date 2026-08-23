using Microsoft.AspNetCore.Mvc;
using Lab2_LTW_CNTT2K65.Models;
using System.Collections.Generic;

namespace Lab2_LTW_CNTT2K65.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            // Tạo danh sách dữ liệu mẫu giống hình minh họa
            List<Product> products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 500000, CreatedAt = "25-12-2020", ImageUrl = "product1.jpeg" },
                new Product { Id = 2, Name = "Product 2", Price = 700000, CreatedAt = "25-12-2020", ImageUrl = "product2.jpeg" },
                new Product { Id = 3, Name = "Product 3", Price = 550000, CreatedAt = "25-12-2020", ImageUrl = "product3.jpeg" },
                new Product { Id = 4, Name = "Product 4", Price = 550000, CreatedAt = "25-12-2020", ImageUrl = "product4.jpeg" }
            };

            return View(products);
        }
    }
}