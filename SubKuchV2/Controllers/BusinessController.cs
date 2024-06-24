using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NuBolet.Models.CommonClasses;
using SubKuchV2.Models;
using SubKuchV2.Models.Dto;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.Controllers
{
    public class BusinessController : Controller
    {
        private readonly ILogger<BusinessController> _Logger;
        private readonly SubKuchDbContext _dbContext;
        private static IHostingEnvironment _environment;

        public BusinessController(ILogger<BusinessController> logger, SubKuchDbContext dbContext, IHostingEnvironment environment)
        {
            _Logger = logger;
            _dbContext = dbContext;
            _environment = environment;
        }
        public IActionResult Businesses()
        {
            var dt = _dbContext.Businesses.Where(a => a.IsActive == true).ToList();
            ViewBag.BusinessData = dt.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddBusiness(string BusinessName, int Id)
        {
            if (Id > 0)
            {
                var nameCheck = _dbContext.Businesses.Where(a => a.Name == BusinessName && a.IsActive == true).FirstOrDefault();
                if (nameCheck != null)
                {
                    return RedirectToAction("Businesses");
                }

                var dt = _dbContext.Businesses.Where(a => a.Id == Id).FirstOrDefault();
                dt.Name = BusinessName;
                _dbContext.Update(dt);
                _dbContext.SaveChanges();
                ViewBag.Msg = "Success";
                return RedirectToAction("Businesses");
            }
            else
            {
                if (!string.IsNullOrEmpty(BusinessName))
                {
                    var nameCheck = _dbContext.Businesses.Where(a => a.Name == BusinessName && a.IsActive == true).FirstOrDefault();
                    if(nameCheck != null)
                    {
                        TempData["Msg"] = "Already Exist !";
                        return RedirectToAction("Businesses");
                    }

                    Business dt = new();
                    dt.Date = DateTime.Now;
                    dt.IsActive = true;
                    dt.Name = BusinessName;
                    _dbContext.Businesses.Add(dt);
                    _dbContext.SaveChanges();
                    ViewBag.Msg = "Success";
                    return RedirectToAction("Businesses");
                }
            }

            return View();
        }

        public IActionResult GetBusiness(int id)
        {
            var dt = _dbContext.Businesses.Where(a => a.Id == id).FirstOrDefault();
            return PartialView("~/Views/Business/_EditBusiness.cshtml", dt);
        }

        [HttpPost]
        public IActionResult RemoveBusiness(int id)
        {
            var dt = _dbContext.Businesses.Where(a => a.Id == id).FirstOrDefault();
            dt.IsActive = false;
            _dbContext.Businesses.Update(dt);
            _dbContext.SaveChanges();

            ViewBag.Msg = "Success";
            return RedirectToAction("Businesses");
        }

        public IActionResult Stores()
        {
            var dt = from t1 in _dbContext.Stores
                     join t2 in _dbContext.Businesses on t1.Bid equals t2.Id
                     where t1.IsActive == true
                     select new { t1.Id, t1.Date, t1.Name, t1.Location, BusinessName = t2.Name,Percentage = t1.Persentage};

            List<StoresDto> lst = new();
            foreach (var item in dt.ToList())
            {
                StoresDto dto = new();
                dto.BusinessName = item.BusinessName;
                dto.Date = item.Date;
                dto.Id = item.Id;
                dto.Location = item.Location;
                dto.Percentage = item.Percentage;
                dto.Name = item.Name;
                lst.Add(dto);
            }
            ViewBag.StoresData = lst;

            ViewBag.Busi = _dbContext.Businesses.Where(a => a.IsActive == true).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult AddStore(StoresDto dto, IFormFile Image)
        {
            if (dto.Id > 0)
            {
                var dt = _dbContext.Stores.Where(a => a.Id == dto.Id).FirstOrDefault();
                var filename = "";
                if (Image != null)
                {
                    Random rnd = new();
                    var rn = rnd.Next(111, 999);
                    var fname = rn + CommonMethods.RemoveWhitespace(Image.FileName.ToString());
                    using FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\content\\images\\Store\\" + fname);
                    Image.CopyTo(fileStream);
                    var pathh = Path.Combine(_environment.WebRootPath, "\\content\\images\\Store\\" + Image.FileName);
                    filename = fname;
                    fileStream.Flush();
                    dt.Image = filename;
                }

                dt.Bid = dto.BusinId;
                dt.Location = dto.Location;
                dt.Name = dto.Name;
                dt.Persentage = dto.Percentage;
                _dbContext.Stores.Update(dt);
                _dbContext.SaveChanges();
            }
            else
            {
                var BusinessName = _dbContext.Businesses.Where(a => a.Id == dto.BusinId).FirstOrDefault();
                if(BusinessName.Name != "Foods" && BusinessName.Name != "Bakers")
                {
                    var store = _dbContext.Stores.Where(a => a.Bid == dto.BusinId).FirstOrDefault();
                    if (store != null)
                    {
                        TempData["Msg"] = "Only One Food Can Be Added IF Businesses Are Other Than Foods & Bakers !";
                        return RedirectToAction("Stores");
                    }

                }

                Store dts = new();
                var filename = "";
                if (Image != null)
                {
                    Random rnd = new();
                    var rn = rnd.Next(111, 999);
                    var fname = rn + CommonMethods.RemoveWhitespace(Image.FileName.ToString());
                    using FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\content\\images\\Store\\" + fname);
                    Image.CopyTo(fileStream);
                    var pathh = Path.Combine(_environment.WebRootPath, "\\content\\images\\Store\\" + Image.FileName);
                    filename = fname;
                    fileStream.Flush();
                    dts.Image = filename;
                }


                dts.Bid = dto.BusinId;
                dts.Date = DateTime.Now;
                dts.IsActive = true;
                dts.Location = dto.Location;
                dts.Persentage = dto.Percentage;
                dts.Name = dto.Name;
                _dbContext.Stores.Add(dts);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Stores");
        }
        public IActionResult GetStore(int id)
        {
            var dt = _dbContext.Stores.Where(a => a.Id == id).FirstOrDefault();
            ViewBag.Business = _dbContext.Businesses.Where(a => a.Id == dt.Bid).ToList();
            ViewBag.Busi = _dbContext.Businesses.Where(a => a.IsActive == true).ToList();
            return PartialView("~/Views/Business/_EditStore.cshtml", dt);
        }

        [HttpPost]
        public IActionResult RemoveStore(int id)
        { 
            var dt = _dbContext.Stores.Where(a => a.Id == id).FirstOrDefault();
            dt.IsActive = false;
            _dbContext.Stores.Update(dt);
            _dbContext.SaveChanges();

            ViewBag.Msg = "Success";
            return RedirectToAction("Stores");
        }
    }
}
