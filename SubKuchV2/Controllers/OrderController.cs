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
using System.Dynamic;
namespace SubKuchV2.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _Logger;
        private readonly SubKuchDbContext _dbContext;
        private static IHostingEnvironment _environment;

        public OrderController(ILogger<OrderController> logger, SubKuchDbContext dbContext, IHostingEnvironment environment)
        {
            _Logger = logger;
            _dbContext = dbContext;
            _environment = environment;
        }
        public IActionResult Order()
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");

            var dt = from t1 in _dbContext.Orders
                     join t2 in _dbContext.Stores on t1.Sid equals t2.Id
                     join t3 in _dbContext.OrderPrices on t1.Id equals t3.Oid
                     join t4 in _dbContext.OrderStatuses on t1.Id equals t4.Oid


                     where t1.IsActive == true && t3.IsActive == true
                     select new
                     {
                         t1.Id,
                         t1.Date,
                         t1.CustomerName,
                         t1.CustomerPhone,
                         t1.Location,
                         StoreName = t2.Name,
                         Discount = t3.DiscountPrice,
                         Status = t4.Status

                     };
            List<OrderDto> lst = new();
            foreach (var item in dt.ToList())
            {
                OrderDto dto = new();
                dto.Id = item.Id;
                dto.CustomerName = item.CustomerName;
                dto.CustomerPhone = item.CustomerPhone;
                dto.Location = item.Location;
                dto.Date = item.Date;
                dto.StoreName = item.StoreName;
                dto.DiscountPrice = item.Discount;
                dto.OrderStatus = item.Status;

                lst.Add(dto);
            }


            ViewBag.OrderData = lst;

            return View();
        }
        public IActionResult GetOrderDetails(int Oid)
        {

            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");

            ViewBag.Ord = _dbContext.Orders.Where(a => a.Id == Oid).ToList();

            List<Order> ordersList = _dbContext.Orders.Where(x => x.Id == Oid).ToList();
            List<OrderItme> orderItemsList = _dbContext.OrderItmes.ToList();
            List<Product> productsList = _dbContext.Products.ToList();
            List<OrderPrice> orderPricesList = _dbContext.OrderPrices.ToList();
            List<Price> pricesList = _dbContext.Prices.ToList();

            //var Orders = _dbContext.Orders.Where(a => a.Id == Id).ToList();
            //var OrderItems = _dbContext.OrderItmes.ToList();
            //var Products = _dbContext.Products.ToList();
            //var OrderPrices = _dbContext.OrderPrices.ToList();
            //var Prices = _dbContext.Prices.ToList();

            List<Store> storesList = _dbContext.Stores.ToList();


           ViewData["join"]=from s1 in ordersList
                            join s2 in storesList on s1.Sid equals s2.Id
                            into table1
                            from s2 in table1.DefaultIfEmpty()

                            select
                            new MultiTablesJoinClass
                            {
                                storesList = s2
                            };

           

            ViewData["jointables"] = from t1 in ordersList
                                     join t3 in orderItemsList on t1.Id equals t3.Oid
                                     join t4 in productsList on t3.PrId equals t4.Id
                                     join t5 in orderPricesList on t1.Id equals t5.Oid
                                     join t6 in pricesList on t3.PrId equals t6.PrId
                                     into table
                                     from t6 in table.DefaultIfEmpty()

                                     select
                                     new MultiTablesJoinClass
                                     {
                                         ordersList = t1,
                                         orderItemsList = t3,
                                         productsList = t4,
                                         orderPricesList = t5,
                                         pricesList = t6
                          //t1.Id,
                          //quantity = t3.Quantity,
                          //ProductName = t4.Name,
                          //discount = t5.DiscountPrice,
                          //totalPrice = t5.TotalPrice,
                          //SalePrice = t6.SalePrice

                      };

            var khhb = ViewData["jointables"];

            //List<OrderDto> lsst = new();
            //foreach (var item in dtt.ToList())
            //{
            //    OrderDto dto = new();

            //    dto.Id = item.Id;
            //    dto.ProductName = item.ProductName;
            //    dto.Quantity = item.quantity;
            //    dto.SalePrice = item.SalePrice;
            //    dto.DiscountPrice = item.SalePrice - item.discount;
            //    dto.netamount = item.totalPrice * item.quantity;


            //    lsst.Add(dto);
            //}

            //ViewBag.orderdatta = ViewData["jointables"];

            return PartialView("~/Views/Order/_EditOrderDetails.cshtml");
        }

        public IActionResult AssignOrder(int Id)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");

            var Riders = _dbContext.Riders.Where(x => x.IsActive == true).ToList();
            var OrderId = _dbContext.Orders.Where(x => x.Id == Id).ToList();


            ViewBag.OrderId = OrderId;
            ViewBag.Riders = Riders;

            return View();
        }


        public IActionResult AcceptedOrder(int OId, int Id)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");

            var orderStatus = _dbContext.OrderStatuses.Where(x => x.Oid == OId).FirstOrDefault();
            var riderStatus = _dbContext.Riders.Where(a => a.Id == Id).FirstOrDefault();

            riderStatus.Status = "Assigned";
            _dbContext.Update(riderStatus);

            orderStatus.Status = "InProcess";
            _dbContext.Update(orderStatus);

            _dbContext.SaveChanges();
            return RedirectToAction("AssignOrder");
        }

        public IActionResult Accepted()
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");

            var acceptedOrders = from t1 in _dbContext.Orders
                                 join t2 in _dbContext.OrderStatuses on t1.Id equals t2.Oid

                                 where t1.IsActive == true && t2.IsActive == true

                                 select new
                                 {
                                     t1.Id,
                                     t1.CustomerName,
                                     t1.CustomerPhone,
                                     OrderStatus = t2.Status
                                 };

            List<OrderDto> list = new();
            foreach (var item in acceptedOrders.ToList())
            {
                OrderDto dto = new();

                dto.Id = item.Id;
                dto.CustomerName = item.CustomerName;
                dto.CustomerPhone = item.CustomerPhone;
                dto.OrderStatus = item.OrderStatus;

                list.Add(dto);
            }

            ViewBag.Acceptedorders = list;

            return View();
        }

        public IActionResult RejectedOrders(int Id)
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");

            var dt = _dbContext.Orders.Where(a => a.Id == Id).FirstOrDefault();
            dt.IsActive = false;
            _dbContext.Orders.Update(dt);
            _dbContext.SaveChanges();

            return RedirectToAction("Order");
        }

        
        public IActionResult RejectedOrder()
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");

            var dt = _dbContext.Orders.Where(a => a.IsActive == false).ToList();

            ViewBag.Rejected = dt;

            return View();
        }

        public IActionResult CompletedOrders()
        {
            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");

            var completedOrders = from t1 in _dbContext.Orders
                                 join t2 in _dbContext.OrderStatuses on t1.Id equals t2.Oid

                                 where t1.IsActive == true && t2.IsActive == true

                                 select new
                                 {
                                     t1.Id,
                                     t1.CustomerName,
                                     t1.CustomerPhone,
                                     OrderStatus = t2.Status
                                 };

            List<OrderDto> list = new();
            foreach (var item in completedOrders.ToList())
            {
                OrderDto dto = new();

                dto.Id = item.Id;
                dto.CustomerName = item.CustomerName;
                dto.CustomerPhone = item.CustomerPhone;
                dto.OrderStatus = item.OrderStatus;

                list.Add(dto);
            }

            ViewBag.CompletedOrders = list;

            return View();
        }


        public IActionResult ProductsDecrease(int Id)
        {

            var usId = HttpContext.Session.GetInt32("userId");
            if (usId == null) return RedirectToAction("Login", "Home");


            var ItemsDecrease = _dbContext.OrderItmes.Where(x => x.Oid == Id).FirstOrDefault();

            if(ItemsDecrease.Quantity == 1)
            {
                return null;
            }

            ItemsDecrease.Quantity = ItemsDecrease.Quantity - 1;

            _dbContext.OrderItmes.Update(ItemsDecrease);
            _dbContext.SaveChanges();

            ViewBag.Ord = _dbContext.Orders.Where(a => a.Id == Id).ToList();

            List<Order> ordersList = _dbContext.Orders.Where(x => x.Id == Id).ToList();
            List<OrderItme> orderItemsList = _dbContext.OrderItmes.ToList();
            List<Product> productsList = _dbContext.Products.ToList();
            List<OrderPrice> orderPricesList = _dbContext.OrderPrices.ToList();
            List<Price> pricesList = _dbContext.Prices.ToList();

            List<Store> storesList = _dbContext.Stores.ToList();


            ViewData["join"] = from s1 in ordersList
                               join s2 in storesList on s1.Sid equals s2.Id
                               into table1
                               from s2 in table1.DefaultIfEmpty()

                               select
                               new MultiTablesJoinClass
                               {
                                   storesList = s2
                               };



            ViewData["jointables"] = from t1 in ordersList
                                     join t3 in orderItemsList on t1.Id equals t3.Oid
                                     join t4 in productsList on t3.PrId equals t4.Id
                                     join t5 in orderPricesList on t1.Id equals t5.Oid
                                     join t6 in pricesList on t3.PrId equals t6.PrId
                                     into table
                                     from t6 in table.DefaultIfEmpty()

                                     select
                                     new MultiTablesJoinClass
                                     {
                                         ordersList = t1,
                                         orderItemsList = t3,
                                         productsList = t4,
                                         orderPricesList = t5,
                                         pricesList = t6


                                     };
            

            return PartialView("~/Views/Order/_EditOrderDetails.cshtml");

        }

    }
}
