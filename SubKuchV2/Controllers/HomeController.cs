using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SubKuchV2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SubKuchDbContext _dbContext;
        public HomeController(ILogger<HomeController> logger, SubKuchDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            try
            {
                var UsId = HttpContext.Session.GetInt32("userId");
                if (UsId != null)
                {
                    return View();
                }
                return RedirectToAction("Login", "Home");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string Email, string Password)
        
        
         {
            var dt = _dbContext.Users.Where(a => a.Email == Email && a.Password == Password && a.IsActive == true).FirstOrDefault();
            if (dt != null)
            {
                HttpContext.Session.SetInt32("userId", dt.Id);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userId");
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
