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
    public class PaymentController : Controller
    {

        private readonly ILogger<PaymentController> _Logger;
        private readonly SubKuchDbContext _dbContext;
        private static IHostingEnvironment _environment;

        public PaymentController(ILogger<PaymentController> logger, SubKuchDbContext dbContext, IHostingEnvironment environment)
        {
            _Logger = logger;
            _dbContext = dbContext;
            _environment = environment;
        }

        public IActionResult Payment()
        {
            return View();
        }
      
        [HttpPost]
        public IActionResult GetPayments(DateTime strt, DateTime end)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");

            List<OrderPrice> orderPricesList = _dbContext.OrderPrices.Where(a => a.Date >= strt && a.Date <= end).ToList();
            List<Order> ordersList = _dbContext.Orders.ToList();
            List<OrderStatus> statusList = _dbContext.OrderStatuses.ToList();

            ViewData["joinTables"] = from t1 in orderPricesList
                                     join t2 in ordersList on t1.Oid equals t2.Id
                                     join t3 in statusList on t2.Id equals t3.Oid
                                     where t3.Status == "Delivered"

                                     select
                                     new MultiTablesJoinClass
                                     {
                                         orderPricesList = t1,
                                         ordersList = t2
                                     };



            return PartialView("~/Views/Payment/_Payments.cshtml");
        }
    }
}
