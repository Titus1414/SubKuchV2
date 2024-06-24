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
    public class CouponCodeController : Controller
    {
        private readonly SubKuchDbContext _dbContext;
        private static IHostingEnvironment _environment;
        public CouponCodeController(SubKuchDbContext dbContext, IHostingEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }
        public IActionResult CouponCode()
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            ViewBag.Codes = _dbContext.CouponCodes.Where(a => a.IsActive == true).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult AddCouponCode(CouponCode  dto)
        {

            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            if (dto.Id > 0)
            {
                var dt = _dbContext.CouponCodes.FirstOrDefault(a => a.Id == dto.Id);
                if (dt != null)
                {
                    dt.Code= dto.Code;
                    dt.MinimumOrder = dto.MinimumOrder;
                    dt.DiscountedPrice = dto.DiscountedPrice;
                    dt.CurrentStatus = dto.CurrentStatus;
                    dt.ValidityDate = dto.ValidityDate;
                    dt.Date = dto.Date;
                    _dbContext.CouponCodes.Update(dt);
                    _dbContext.SaveChanges();
                }
            }
            else
            {
                CouponCode dt = new()
                {
                    Code = dto.Code,
                    MinimumOrder = dto.MinimumOrder,
                    DiscountedPrice = dto.DiscountedPrice,
                    CurrentStatus = false,
                    ValidityDate = dto.ValidityDate,
                    Date = DateTime.Now,
                    IsActive = true
                };
                _dbContext.CouponCodes.Add(dt);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("CouponCode");
        }
        public IActionResult ChangeStatus(int id)
        {
            var dt = _dbContext.CouponCodes.Where(a => a.Id == id).FirstOrDefault();
            if (dt.CurrentStatus== true)
            {
                dt.CurrentStatus = false;
            }
            else
            {
                dt.CurrentStatus = true;

            }
            _dbContext.CouponCodes.Update(dt);
            _dbContext.SaveChanges();

            return RedirectToAction("CouponCode");
        }

        public IActionResult GetCouponCode(int id)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            var dt = _dbContext.CouponCodes.Where(a => a.Id == id).FirstOrDefault();
            return PartialView("~/Views/CouponCode/_EditCouponCode.cshtml", dt);
        }
        public IActionResult RemoveCode(int id)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            var dt = _dbContext.CouponCodes.FirstOrDefault(a => a.Id == id);
            if (dt != null)
            {
                dt.IsActive = false;
                _dbContext.CouponCodes.Update(dt);
            }

            _dbContext.SaveChanges();

            ViewBag.Msg = "Success";
            return RedirectToAction("CouponCode");
        }
    }
}
