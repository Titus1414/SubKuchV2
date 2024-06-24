using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ProductController : Controller
    {
        private readonly SubKuchDbContext _dbContext;
        private static IHostingEnvironment _environment;
        public ProductController(SubKuchDbContext dbContext, IHostingEnvironment environment)
        { 
            _dbContext = dbContext;
            _environment = environment;
        }
        public IActionResult Products()
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            ViewBag.ProductData = _dbContext.Products.Where(a => a.IsActive == true).ToList();
            ViewBag.Stores = _dbContext.Stores.Where(a => a.IsActive == true).ToList();
            ViewBag.Categories = _dbContext.ProductCatagories.Where(a => a.IsActive == true).ToList();
            var dt = from t1 in _dbContext.Products
                     join t2 in _dbContext.ProductCatagories on t1.PcId equals t2.Id
                      join t3 in _dbContext.Prices on t1.Id equals t3.PrId
                     join t4 in _dbContext.Stores on t2.Sid equals t4.Id
                     where t1.IsActive == true && t3.IsActive == true
                     select new { t1.Id, CatName = t2.Name, t1.Name, t1.Date, t3.PurchasePrice, t3.SalePrice, StoreName = t4.Name };
            List<ProductDto> lst = new();
            foreach (var item in dt.ToList())
            {

                ProductDto dto = new();
                dto.CatName = item.CatName;
                dto.Id = item.Id;
                dto.Date = item.Date;
                dto.Product = item.Name;
                dto.PurchasePrice = item.PurchasePrice;
                dto.SalePrice = item.SalePrice;
                dto.StoreName = item.StoreName;
               lst.Add(dto);
                
            }
            ViewBag.ProductData = lst;
            ViewBag.Products = _dbContext.Products.Where(a => a.IsActive == true).ToList();
            return View();
        }
        public IActionResult GetCategories(int id)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            ViewBag.Categories = _dbContext.ProductCatagories.Where(a => a.Sid == id && a.IsActive == true).ToList();
            return PartialView("~/Views/Product/_CategoriesDropdown.cshtml");
        }

        [HttpPost]
        public IActionResult AddProduct(ProductDto dto, IFormFile image)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");

            if (dto.Id > 0)
            {
                var pt = _dbContext.Prices.FirstOrDefault(a => a.PrId == dto.Id);
                var dt = _dbContext.Products.FirstOrDefault(a => a.Id == dto.Id);
                var filename = "";
                if (image != null)
                {
                    Random rnd = new();
                    var rn = rnd.Next(111, 999);
                    var fname = rn + CommonMethods.RemoveWhitespace(image.FileName.ToString());
                    using FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\content\\images\\Store\\" + fname);
                    image.CopyTo(fileStream);
                    Path.Combine(_environment.WebRootPath, "\\content\\images\\Store\\" + image.FileName);
                    filename = fname;
                    fileStream.Flush();
                    if (dt != null) dt.Image = filename;
                }
                dt.PcId = dto.CatId;
                dt.Name = dto.Product;
                pt.PurchasePrice = dto.PurchasePrice;
                pt.SalePrice = dto.SalePrice;
                _dbContext.Products.Update(dt);
                _dbContext.Prices.Update(pt);
                _dbContext.SaveChanges();
            }
            else
            {

                Product dt = new();
                dt.PcId = dto.CatId;
                dt.Name = dto.Product;
                dt.Date = DateTime.Now;
                dt.IsActive = true;

                var filename = "";
                if (image != null)
                {
                    Random rnd = new();
                    var rn = rnd.Next(111, 999);
                    var fname = rn + CommonMethods.RemoveWhitespace(image.FileName.ToString());
                    using FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\content\\images\\Store\\" + fname);
                    image.CopyTo(fileStream);
                    Path.Combine(_environment.WebRootPath, "\\content\\images\\Store\\" + image.FileName);
                    filename = fname;
                    fileStream.Flush();
                    dt.Image = filename;
                }
                _dbContext.Products.Add(dt);
                _dbContext.SaveChanges();

                int Pid = _dbContext.Products.Max(a => a.Id);
                Price prc = new();
                prc.PrId = Pid;
                prc.PurchasePrice = dto.PurchasePrice;
                prc.SalePrice = dto.SalePrice;
                prc.IsActive = true;
                prc.Date = DateTime.Now;
                _dbContext.Prices.Add(prc);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Products");

        }
        public IActionResult GetProduct(int id)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            ViewBag.Stores = _dbContext.Stores.Where(a => a.IsActive == true).ToList();
            var dt = _dbContext.Products.FirstOrDefault(a => a.Id == id);
            var ct = _dbContext.ProductCatagories.FirstOrDefault(a => a.Id == dt.PcId);
            ViewBag.Price = _dbContext.Prices.Where(a => a.PrId == id).ToList();
            ViewBag.Category = ct.Name;
            ViewBag.CategoryId = ct.Id;
            ViewBag.Store = _dbContext.Stores.FirstOrDefault(a => a.Id == ct.Sid).Name;
            ViewBag.Id = _dbContext.Stores.FirstOrDefault(a => a.Id == ct.Sid).Id;


            return PartialView("~/Views/Product/_EditProducts.cshtml", dt);
        }
        
        public IActionResult GetProductPrice(int id)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            var dt = _dbContext.Prices.FirstOrDefault(a => a.PrId == id);
            return PartialView("~/Views/Product/_EditProductPrice.cshtml", dt);
        }

        [HttpPost]
        public IActionResult RemoveProduct(int id)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            var dt = _dbContext.Products.FirstOrDefault(a => a.Id == id);
            if (dt != null)
            {
                dt.IsActive = false;
                _dbContext.Products.Update(dt);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Products");
        }
        public IActionResult UpdatePrices(ProductDto dto)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            var dt = _dbContext.Prices.FirstOrDefault(a => a.Id == dto.Id);
            dt.IsActive = false;
            _dbContext.Prices.Update(dt);
            _dbContext.SaveChanges();

            Price prc = new();
            prc.PrId = dto.Id;
            prc.PurchasePrice = dto.PurchasePrice;
            prc.SalePrice = dto.SalePrice;
            prc.IsActive = true;
            prc.Date = DateTime.Now;
            _dbContext.Prices.Add(prc);
            _dbContext.SaveChanges();

            return RedirectToAction("Products");
        }
       

    }
}
