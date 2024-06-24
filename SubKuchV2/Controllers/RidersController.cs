using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SubKuchV2.Models;
using SubKuchV2.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.Controllers
{
    public class RidersController : Controller
    {
        private readonly SubKuchDbContext _dbContext;
        private static IHostingEnvironment _environment;
        public RidersController(SubKuchDbContext dbContext, IHostingEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }
        public IActionResult Rider()
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");

            ViewBag.Riders = _dbContext.Riders.Where(a => a.IsActive == true).ToList();

            return View();
        }
        [HttpPost]
        public IActionResult AddRider(Rider dto)
        {

            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            if (dto.Id > 0)
            {
                var dt = _dbContext.Riders.Where(a => a.Id == dto.Id).FirstOrDefault();
                if (dt != null)
                {
                    dt.Name= dto.Name;
                    dt.Phone = dto.Phone;
                    dt.Cnic = dto.Cnic;
                    dt.ModelNumber= dto.ModelNumber;
                    dt.Vehical = dto.Vehical;
                    dt.Password = dto.Password;
                    dt.Date = DateTime.Now;
                    _dbContext.Riders.Update(dt);
                    _dbContext.SaveChanges();
                }
            }
            else
            {
                Rider dt = new()
                {
                    Name = dto.Name,
                    Phone = dto.Phone,
                    Cnic = dto.Cnic,
                    ModelNumber = dto.ModelNumber,
                    Vehical = dto.Vehical,
                    Password = dto.Password,
                    Date = DateTime.Now,
                    IsActive = true,
                    Status = "Free",
                    IsOnline = false
                };
                _dbContext.Riders.Add(dt);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Rider");
        }

        public IActionResult GetRider(int id)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            var dt = _dbContext.Riders.Where(a => a.Id == id).FirstOrDefault();
            return PartialView("~/Views/Riders/_EditRider.cshtml", dt);
        }
        [HttpPost]
        public IActionResult RemoveRider(int id)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");
            var dt = _dbContext.Riders.FirstOrDefault(a => a.Id == id);
            if (dt != null)
            {
                dt.IsActive = false;
                _dbContext.Riders.Update(dt);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Rider");
        }
    }
    }

