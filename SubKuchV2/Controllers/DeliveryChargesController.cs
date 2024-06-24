using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubKuchV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.Controllers
{
    public class DeliveryChargesController : Controller
    {
        private readonly SubKuchDbContext _dbContext;
        private static IHostingEnvironment _environment;
        public DeliveryChargesController(SubKuchDbContext dbContext, IHostingEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }
        public IActionResult DeliveryCharges()
        {

            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            ViewBag.DeliveryCharges = _dbContext.DeliveryCharges.Where(a => a.IsActive == true).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult AddDeliveryCharges(DeliveryCharge dto)
        {

            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            if (dto.Id > 0)
            {
                var dt = _dbContext.DeliveryCharges.Where(a => a.Id == dto.Id).FirstOrDefault();
                dt.Date = DateTime.Now;
                dt.IsActive = true;
                dt.Kilomitter = dto.Kilomitter;
                dt.Price = dto.Price;
                dt.Title = dto.Title;
                dt.Id = dto.Id;
                _dbContext.DeliveryCharges.Update(dt);
                _dbContext.SaveChanges();

                //DeliveryCharge dc = new();
                //dc.Date = DateTime.Now;
                //dc.IsActive = true;
                //dc.Kilomitter = dto.Kilomitter;
                //dc.Price = dto.Price;
                //dc.Title = dto.Title;
                //_dbContext.DeliveryCharges.Add(dc);
                //_dbContext.SaveChanges();
            }
            return RedirectToAction("DeliveryCharges");
        }
        public IActionResult GetDeliveryCharge(int id)
         {

            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            var dt = _dbContext.DeliveryCharges.Where(a => a.Id == id).FirstOrDefault();
            return PartialView("~/Views/DeliveryCharges/_EditDeliveryCharges.cshtml", dt);
        }

    }
}
