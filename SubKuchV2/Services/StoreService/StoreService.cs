using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SubKuchV2.DTO_s;
using SubKuchV2.Models;
using SubKuchV2.Services.StoreService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SabKuchWeb.Services.StoreService
{
    public class StoreService : IStoreService
    {
        public static IWebHostEnvironment _environment;
        private readonly SubKuchDbContext _dbContext;
        public StoreService(SubKuchDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }
        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
        public async Task<string> AddStore(AddStoreDto dto)
        {


            var sda = _dbContext.Stores.Where(a => a.Name == dto.StoreName && a.IsActive == true).FirstOrDefault();

            if (sda != null)
            {
                return "Already Exist";
            }

            var sd = _dbContext.Businesses.Where(s => s.Id == dto.BuisCatID).FirstOrDefault();
           
            dto.CatName = sd.Name;
           
            dto.StoreType = sd.Name;
            
            var filename = "";
            var filename1 = "";
            Random rnd = new Random();
            var rn = rnd.Next(111, 999);
            if (dto.CatImage != null)
            {

                var CatgoryImage = rn + RemoveWhitespace(dto.CatImage.FileName);
                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Content\\CatgoryImage\\" + CatgoryImage))
                {

                    dto.CatImage.CopyTo(fileStream);
                    var pathh = Path.Combine(_environment.WebRootPath, "\\Content\\CatgoryImage\\" + dto.CatImage.FileName);
                    filename = CatgoryImage;

                    fileStream.Flush();
                     
                }
            }
            //if (dto.ImagePath!= null)
            //{
            //    var ImagePath1 = rn + RemoveWhitespace(dto.ImagePath.FileName);
            //    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Content\\StoreImages\\" + ImagePath1))
            //    {

            //        dto.ImagePath.CopyTo(fileStream);
            //        var pathh = Path.Combine(_environment.WebRootPath, "\\Content\\StoreImages\\" + dto.ImagePath.FileName);
            //        filename1 = ImagePath1;

            //        fileStream.Flush();


            //    }
            //}
            //BussinessCategorey obj = new BussinessCategorey()
            //{
            //    Type = dto.Type,
            //    CatName = dto.CatName,
            //    CatImage = filename1,
            //    status = true

            //};
            // _dbContext.BussinessCategoreys.Add(obj);
            //await _dbContext.SaveChangesAsync();
            //var categoryId =  _dbContext.BussinessCategoreys.Where(w => w.CatName == "Food" && w.status == true).Max(s => s.BussinessCategoreyId);

            Store obj2 = new Store()
            {
                Bid = dto.BuisCatID,
                Name = dto.StoreName,
                Location = dto.StoreLocation,
                Persentage = dto.Percentage,
                Date = DateTime.Now,
                IsOnline = true,
                Image = filename1,
                IsActive = true,
            };
            _dbContext.Stores.Add(obj2);
            await _dbContext.SaveChangesAsync();

            return "success";
        }

        public async Task<object> SearchProduct(string productname)
        {


            var dd = await _dbContext.Products.Where(x => x.IsActive == true && x.Name.Contains(productname)).Select(x => new
            {

                Product = x,
                Storeinfo = _dbContext.Stores.Where(y => y.IsActive == true && y.Id == x.PcId && y.IsOnline == false).SingleOrDefault(),


            }).ToListAsync();



            //var data = await _dbContext.BussinessStores.Where(x => x.IsOnline == true).Select(x => new
            //{
            //    store = _dbContext.storeItems.Where(w => w.ItemName.Contains(productname)).SingleOrDefault(),
            //    menucat = _dbContext.storeMenuCategoreys.Where(q => q.Menu.BussinessStoreId == x.BussinessStoreId).Select(q => new
            //    {
            //        catname = q.Categorey.Category_Name,
            //        catid = q.Category_ID,
            //        menucatid = q.MenuCategory_Id,
            //        item = _dbContext.storeItems.Where(w => w.MenuCategory_Id == q.MenuCategory_Id && w.ItemName.Contains(productname)).ToList()
            //    }).ToList()

            //}).ToListAsync();

            return dd;
        }
        public async Task<string> AddRestaurant(AddStoreDto dto)
        {

            var sda = _dbContext.Stores.Where(a => a.Name == dto.StoreName && a.IsActive == true).FirstOrDefault();

            if (sda != null)
            {
                return "Already Exist";
            }

            var filename1 = "";
            Random rnd = new Random();
            var rn = rnd.Next(111, 999);

            if (dto.ImagePath != null)
            {
                var ImagePath1 = rn + RemoveWhitespace(dto.ImagePath.FileName);
                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Content\\StoreImages\\" + ImagePath1))
                {

                    dto.ImagePath.CopyTo(fileStream);
                    var pathh = Path.Combine(_environment.WebRootPath, "\\Content\\StoreImages\\" + dto.ImagePath.FileName);
                    filename1 = ImagePath1;

                    fileStream.Flush();
                }
            }
            var categoryId = 0;
            var Exits = await _dbContext.Businesses.AnyAsync(x => x.Name == "Food");
            if (Exits == true)
            {
                //categoryId = await _dbContext.BussinessCategoreys.MaxAsync(x => x.BussinessCategoreyId);
                categoryId = dto.BuisCatID;
            }
            else
            {
                Business obj = new Business()
                {
                    Name = "Food",
                    IsActive = true
                };
                await _dbContext.Businesses.AddAsync(obj);
                //categoryId = await _dbContext.BussinessCategoreys.MaxAsync(x => x.BussinessCategoreyId);
                categoryId = dto.BuisCatID;
            }


            Store obj2 = new Store()
            {
                IsOnline = true,
                Bid = categoryId,
                Name = dto.StoreName,
                Location = dto.StoreLocation,
                Persentage = dto.Percentage,
                Date = DateTime.Now,
                Image = filename1,
                IsActive = true
            };
            _dbContext.Stores.Add(obj2);
            await _dbContext.SaveChangesAsync();

            return "success";
        }

        public async Task<object> GetBussinesCat()
        {
            var data = await _dbContext.Businesses.Where(x => x.IsActive == true).ToListAsync();
            return data;
        }

        public async Task<object> catwiseItems(int catid)
         {
            var data = await _dbContext.Stores.Where(x => x.Bid == catid && x.IsOnline == true && x.IsActive == true).Select(x => new
            {
                store = x,
                menucat = _dbContext.ProductCatagories.Where(q => q.Sid == x.Id).Select(q => new
                {
                    catname = q.Name,
                    catid = q.Id,
                    menucatid = q.Id,
                    item = _dbContext.Products.Where(w => w.PcId == q.Id).ToList()
                }).ToList()

            }).ToListAsync();
            return data;
        }
        public async Task<object> GetStoreProduct(int id)
        {
            var data = await _dbContext.Stores.Where(x => x.Id == id && x.IsOnline == true).Select(x => new
            {
                store = x,
                menucat = _dbContext.ProductCatagories.Where(q => q.Sid == x.Id).Select(q => new
                {
                    catname = q.Name,
                    catid = q.Id,
                    menucatid = q.Id,
                    item = _dbContext.Products.Where(w => w.PcId == q.Id).ToList()
                }).ToList()

            }).ToListAsync();
            return data;
        }
        public async Task<object> GetStores()
        {
            try
            {
                var data = await _dbContext.Stores.Where(x => x.Name != "Restaurant" && x.IsActive == true && x.IsActive == true).Include(x => x.ProductCatagories).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                var data = ex.Message;
                throw;
            }

        }

        public async Task<object> Restaurants()
        {
            try
            {
                var data = await _dbContext.Stores.Where(x => x.Name == "Restaurant" && x.IsActive == true && x.IsActive == true).Include(x => x.ProductCatagories).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                var data = ex.Message;
                throw;
            }
        }

        public async Task<string> UpdateStore(UpdatestoreDto dto)
        {
            var filename1 = "";
            Random rnd = new Random();
            var rn = rnd.Next(111, 999);

            if (dto.ImagePath != null)
            {
                var ImagePath1 = rn + RemoveWhitespace(dto.ImagePath.FileName);
                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Content\\StoreImages\\" + ImagePath1))
                {

                    dto.ImagePath.CopyTo(fileStream);
                    var pathh = Path.Combine(_environment.WebRootPath, "\\Content\\StoreImages\\" + dto.ImagePath.FileName);
                    filename1 = ImagePath1;

                    fileStream.Flush();


                }
                var Store = await _dbContext.Stores.FindAsync(dto.BussinessStoreId);
                Store.Image = filename1;
                Store.Name = dto.StoreName;
                Store.Location = dto.StoreLocation;
                Store.Persentage = dto.Percentage;
                _dbContext.Stores.Update(Store);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                var Store = await _dbContext.Stores.FindAsync(dto.BussinessStoreId);

                Store.Name = dto.StoreName;
                Store.Location = dto.StoreLocation;
                Store.Persentage = dto.Percentage;
                _dbContext.Stores.Update(Store);
                await _dbContext.SaveChangesAsync();
            }


            return "Success";
        }

        public async Task<string> RemoveStore(int Id)
        {
            var dat = await _dbContext.Stores.FindAsync(Id);
            dat.IsActive = !dat.IsActive;
            _dbContext.Stores.Update(dat);
            await _dbContext.SaveChangesAsync();
            return "success";
        }

        public async Task<string> StatusChange(int Id)
        {
            var dat = await _dbContext.Stores.FindAsync(Id);
            dat.IsOnline = !dat.IsOnline;
            _dbContext.Stores.Update(dat);
            await _dbContext.SaveChangesAsync();
            return "success";
        }

        public async Task<List<PaymentDto>> Payment()
        {
            var dt = from t1 in _dbContext.Stores
                     join t2 in _dbContext.Orders on t1.Id equals t2.Id
                     join t3 in _dbContext.OrderPrices on t2.Id equals t3.Oid
                     group new { t1.Persentage, t1.Name, t3.TotalPrice } by new { t1.Name, t1.Persentage } into g
                     select new { g.Key.Name, Payment = g.Sum(a => a.TotalPrice) - (g.Key.Persentage * g.Sum(a => a.TotalPrice) / 100), PercentagePayment = g.Key.Persentage * g.Sum(a => a.TotalPrice) / 100 };

            dt.ToList();
            List<PaymentDto> lst = new List<PaymentDto>();
            foreach (var item in dt)
            {
                PaymentDto dd = new PaymentDto();
                dd.Payment = (int?)item.Payment;
                dd.PercentagePayment = (int?)item.PercentagePayment;
                dd.StoreName = item.Name;
                lst.Add(dd);
            }

            return lst;
        }

        public async Task<List<PaymentDto>> Payment(DateTime strt, DateTime end)
        {
            var dt = from t1 in _dbContext.Stores
                     join t2 in _dbContext.Orders on t1.Id equals t2.Id
                     join t3 in _dbContext.OrderPrices on t2.Id equals t3.Oid
                     where (t3.Date >= strt.Date && t3.Date <= end) && t1.IsActive == true
                     group new { t1.Persentage, t1.Name, t3.TotalPrice } by new { t1.Name, t1.Persentage } into g
                     select new { g.Key.Name, Payment = g.Sum(a => a.TotalPrice) - (g.Key.Persentage * g.Sum(a => a.TotalPrice) / 100), PercentagePayment = g.Key.Persentage * g.Sum(a => a.TotalPrice) / 100 };

            dt.ToList();
            List<PaymentDto> lst = new List<PaymentDto>();
            foreach (var item in dt)
            {
                PaymentDto dd = new PaymentDto();
                dd.Payment = (int?)item.Payment;
                dd.PercentagePayment = (int?)item.PercentagePayment;
                dd.StoreName = item.Name;
                lst.Add(dd);
            }

            return lst;
        }



    }
}