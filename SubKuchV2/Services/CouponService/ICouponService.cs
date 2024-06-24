using SubKuchV2.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.Services.CouponService
{
   public  interface ICouponService
    {
        Task<string> AddCoupon(AddCouponDto Dto);
        Task<string> Remove(int Id);
        //Task<object> CheckCoupan(string num, int Id);
        //Task <object> GetUsedCoupons();
        Task<object> AllCoupons();

    }
}
