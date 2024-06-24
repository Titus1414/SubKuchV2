using Microsoft.EntityFrameworkCore;
using SubKuchV2.DTO_s;
using SubKuchV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.Services.CouponService
{
    public class CouponService : ICouponService
    {
        private readonly SubKuchDbContext _dbContext;
        public CouponService(SubKuchDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<object> AllCoupons()
        {
            var coupons = await _dbContext.CouponCodes.ToListAsync();
            return coupons;
            
        }
        //public async Task<object> GetUsedCoupons()
        //{
        //    var coupons = await _dbContext.CouponCodes.Include(x => x.Code).ToListAsync();
        //    return coupons;
        //}

        public async Task<string> Remove(int Id)
        {
            var dat = await _dbContext.CouponCodes.FindAsync(Id);
            _dbContext.CouponCodes.Remove(dat);
            await _dbContext.SaveChangesAsync();
            return "success";

        }



        public async Task<string> AddCoupon(AddCouponDto Dto)
        {
            CouponCode obj = new CouponCode()
            {
                DiscountedPrice = Dto.DiscountedAmount,
                Date = DateTime.Now,
                ValidityDate = Dto.ValidityDate,
                Code = Dto.CuoponCode,
                MinimumOrder = Dto.MinimumOrder,
                IsActive = true,
            };
            await _dbContext.CouponCodes.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return "success";

        }

        //public async Task<object> CheckCoupan(string num, int Id)
        //{
        //    try
        //    {
        //        var dt = _dbContext.CouponCodes.Where(a => a.Code == num && a.ValidityDate >= DateTime.Now.Date).FirstOrDefault();
        //        if (dt != null)
        //        {
        //            var UsedTocken = await _dbContext.consumerCouponCodes.AnyAsync(ss => ss.ConsumerId == Id && ss.CouponCode_id == dt.CouponCode_id);
        //            if (UsedTocken)
        //            {
        //                return "Already Used";
        //            }
        //            else
        //            {
        //                ConsumerCouponCodes obj = new ConsumerCouponCodes()
        //                {
        //                    ConsumerId = Id,
        //                    CouponCode_id = dt.CouponCode_id,
        //                    Usage_Date = DateTime.Now,
        //                    OrderId = 1,
        //                    Status = true

        //                };
        //                await _dbContext.consumerCouponCodes.AddAsync(obj);
        //                await _dbContext.SaveChangesAsync();
        //            }
        //            return dt;
        //        }

        //        return "Coupan Expire";


        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}
    }
}
