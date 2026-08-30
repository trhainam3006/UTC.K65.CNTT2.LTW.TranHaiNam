using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Tuan3.Models;

namespace Tuan3.Controllers
{
    [Route("san-pham")]
    public class ProductController : Controller
    {
        private List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category { Id = 1, Name = "Quần Áo" },
                new Category { Id = 2, Name = "Túi xách" },
                new Category { Id = 3, Name = "Đồng hồ" },
                new Category { Id = 4, Name = "Ti vi" },
                new Category { Id = 5, Name = "Tủ lạnh" },
                new Category { Id = 6, Name = "Máy bơm" },
                new Category { Id = 7, Name = "Quạt điện" },
                new Category { Id = 8, Name = "Lò sưởi" }
            };
        }

        private List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Bộ đồ bơi cho trẻ em nam",
                    Image = "/images/products/p1.jpg",
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde iure doloribus natus expedita, laborum blanditiis quaerat repellendus necessitatibus nam quo earum ex suscipit.",
                    Status = true,
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                },
                new Product
                {
                    Id = 2,
                    Name = "Bộ đồ bơi cho trẻ em nữ",
                    Image = "/images/products/p2.jpg",
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde iure doloribus natus expedita, laborum blanditiis quaerat repellendus necessitatibus nam quo earum ex suscipit.",
                    Status = true,
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                },
                new Product
                {
                    Id = 3,
                    Name = "Bộ đồ bơi cho trẻ em từ 3-5 tuổi",
                    Image = "/images/products/p3.jpg",
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde iure doloribus natus expedita, laborum blanditiis quaerat repellendus necessitatibus nam quo earum ex suscipit.",
                    Status = true,
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                },
                new Product
                {
                    Id = 4,
                    Name = "Bộ đồ bơi cho trẻ em thời trang",
                    Image = "/images/products/p4.jpg",
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde iure doloribus natus expedita, laborum blanditiis quaerat repellendus necessitatibus nam quo earum ex suscipit.",
                    Status = true,
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                },
                new Product
                {
                    Id = 5,
                    Name = "Túi thời trang mẫu mới 2021",
                    Image = "/images/products/p5.jpg",
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 2,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde iure doloribus natus expedita, laborum blanditiis quaerat repellendus necessitatibus nam quo earum ex suscipit.",
                    Status = true,
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                },
                new Product
                {
                    Id = 6,
                    Name = "Túi thời trang da cá sấu",
                    Image = "/images/products/p6.jpg",
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 2,
                    Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsa eligendi, voluptatem perspiciatis qui delectus ab unde iure doloribus natus expedita, laborum blanditiis quaerat repellendus necessitatibus nam quo earum ex suscipit.",
                    Status = true,
                    CreatedAt = new DateTime(2021, 7, 15, 12, 0, 0)
                }
            };
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index(int? categoryId)
        {
            var categories = GetCategories();
            var allProducts = GetProducts();
            var products = categoryId.HasValue
                ? allProducts.Where(p => p.CategoryId == categoryId.Value).ToList()
                : allProducts;

            ViewBag.Categories = categories;
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.Products = products;

            return View(products);
        }

        [HttpGet("chi-tiet", Name = "product_detail")]
        [HttpGet("/chi-tiet-san-pham")]
        public IActionResult Detail(int id)
        {
            var allProducts = GetProducts();
            var product = allProducts.FirstOrDefault(p => p.Id == id) ?? allProducts.FirstOrDefault();

            ViewBag.Product = product;
            ViewBag.Categories = GetCategories();

            return View(product);
        }
    }
}
