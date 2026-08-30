using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Tuan3.Models;

namespace Tuan3.Controllers
{
    public class AccountController : Controller
    {
        private List<Account> GetAccounts()
        {
            return new List<Account>
            {
                new Account
                {
                    Id = 1,
                    Name = "Hoàng Anh",
                    Email = "anh@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = "/images/Avatar/02.jfif",
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },
                new Account
                {
                    Id = 2,
                    Name = "Trường Giang",
                    Email = "giang@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = "/images/Avatar/03.jfif",
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },
                new Account
                {
                    Id = 3,
                    Name = "Hoàng Thúy",
                    Email = "thuy@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = "/images/Avatar/04.jfif",
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                }
            };
        }

        public IActionResult Index()
        {
            List<Account> accounts = GetAccounts();
            ViewBag.Accounts = accounts;
            ViewBag.accounts = accounts;
            return View();
        }

        [Route("ho-so-cua-toi", Name = "profile")]
        public IActionResult Profile(int id)
        {
            List<Account> accounts = GetAccounts();
            Account? account = accounts.FirstOrDefault(ac => ac.Id == id);
            if (account == null)
            {
                account = accounts.FirstOrDefault();
            }
            ViewBag.account = account;
            return View();
        }
    }
}
