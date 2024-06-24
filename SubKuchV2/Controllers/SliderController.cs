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
    public class SliderController : Controller
    { 
            private readonly SubKuchDbContext _dbContext;
            private static IHostingEnvironment _environment;
            public SliderController(SubKuchDbContext dbContext, IHostingEnvironment environment)
            {
                _dbContext = dbContext;
                _environment = environment;
            }
            public IActionResult Slider()
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            ViewBag.Sliders = _dbContext.Sliders.Where(a => a.IsActive == true).ToList();
            ViewBag.Stores = _dbContext.Stores.Where(a => a.IsActive == true).ToList();
            return View();
        }
        public IActionResult AddSlider(SliderDto dto, IFormFile ImagePath)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            if (dto.Id > 0)
            {

            }
            else
            {
                Slider dt = new();

                var filename = "";
                if (ImagePath != null)
                {
                    Random rnd = new();
                    var rn = rnd.Next(111, 999);
                    var fname = rn + CommonMethods.RemoveWhitespace(ImagePath.FileName.ToString());
                    using FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\content\\images\\AppBanners\\" + fname);
                    ImagePath.CopyTo(fileStream);
                    Path.Combine(_environment.WebRootPath, "\\content\\images\\AppBanners\\" + ImagePath.FileName);
                    filename = fname;
                    fileStream.Flush();
                    dt.Image = filename;
                } 
                dt.Date = DateTime.Now;
                dt.IsActive = true;
                dt.Id = dto.Id;
                dt.Sid = dto.Sid;
                _dbContext.Sliders.Add(dt);
                _dbContext.SaveChanges();

            }
            return RedirectToAction("Slider");
        }


        public IActionResult RemoveSlider(int id)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            var dt = _dbContext.Sliders.FirstOrDefault(a => a.Id == id);
            if (dt != null)
            {
                dt.IsActive = false;
                _dbContext.Sliders.Update(dt);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Slider");
        }
    }
}
