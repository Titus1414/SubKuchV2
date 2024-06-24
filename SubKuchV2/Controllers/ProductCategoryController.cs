using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NuBolet.Models.CommonClasses;
using SubKuchV2.Models;
using SubKuchV2.Models.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly ILogger<ProductCategoryController> _logger;
        private readonly SubKuchDbContext _dbContext;
        private static IHostingEnvironment _environment;

        public ProductCategoryController(ILogger<ProductCategoryController> logger, SubKuchDbContext dbContext, IHostingEnvironment environment)
        {
            _logger = logger;
            _dbContext = dbContext;
            _environment = environment;
        }

        public IActionResult ProductCategories()
        {

            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            var dt = from t1 in _dbContext.ProductCatagories
                     join t2 in _dbContext.Stores on t1.Sid equals t2.Id
                     where t1.IsActive == true
                     select new { t1.Id, StoreName = t2.Name, CatName = t1.Name, t1.Date };

            List<CategoryDto> lst = new();
            foreach (var item in dt.ToList())
            {
                CategoryDto dto = new()
                {
                    StoreName = item.StoreName,
                    Category = item.CatName,
                    Date = item.Date,
                    Id = item.Id

                };
                lst.Add(dto);
            }

            ViewBag.dt = lst;

            ViewBag.Stores = _dbContext.Stores.Where(a => a.IsActive == true).ToList();

            return View();
        }

        public IActionResult AddCategory(ProductCatagory model, int Id)
        {
            if (Id > 0)
            {
                var dt = _dbContext.ProductCatagories.Where(a => a.Id == Id).FirstOrDefault();
                dt.Name = model.Name;
                dt.IsActive = true;
                dt.Date = DateTime.Now;
                dt.Sid = model.Sid;
                _dbContext.Update(dt);
                _dbContext.SaveChanges();
                ViewBag.Msg = "Success";
                return RedirectToAction("ProductCategories");
            }
            else
            {
                ProductCatagory dt = new();
                dt.Date = DateTime.Now;
                dt.IsActive = true;
                dt.Name = model.Name;
                dt.Sid = model.Sid;
                _dbContext.ProductCatagories.Add(dt);
                _dbContext.SaveChanges();
                ViewBag.Msg = "Success";

            }
            return RedirectToAction("ProductCategories");
        }


        public IActionResult GetCategory(int id)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            var dt = _dbContext.ProductCatagories.FirstOrDefault(a => a.Id == id);
            ViewBag.SName = _dbContext.Stores.FirstOrDefault(a => a.Id == dt.Sid).Name;
            ViewBag.Stores = _dbContext.Stores.Where(a => a.IsActive == true).ToList();
            return PartialView("~/Views/ProductCategory/_EditCategory.cshtml", dt);
        }
        [HttpPost]

        [HttpPost]
        public IActionResult RemoveCategory(int id)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            var dt = _dbContext.ProductCatagories.FirstOrDefault(a => a.Id == id);
            if (dt != null)
            {
                dt.IsActive = false;
                _dbContext.ProductCatagories.Update(dt);
            }

            _dbContext.SaveChanges();

            ViewBag.Msg = "Success";
            return RedirectToAction("ProductCategories");
        }
    }
}