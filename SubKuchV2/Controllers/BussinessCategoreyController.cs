using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubKuchV2.Services.CouponService;
using SubKuchV2.Services.SliderService;
using SubKuchV2.Services.StoreService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace SubKuchV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BussinessCategoreyController : ControllerBase
    {
        private readonly IStoreService _service;
        private readonly ISliderService _Sliderservice;
        private readonly ICouponService _couponService;
        public BussinessCategoreyController(IStoreService service, ISliderService Sliderservice, ICouponService couponService)
        {
            _service = service;
            _Sliderservice = Sliderservice;
            _couponService = couponService;
        }
        [HttpGet]
        [Route("GetBussinessCat")]
        public async Task<IActionResult> GetBussinessCat()
        {

            var result = await _service.GetBussinesCat();

            return Ok(new { res = result });


        }

        [HttpGet]
        [Route("GetSliders")]
        public async Task<IActionResult> GetSliders()
        {

            var result = await _Sliderservice.GetSliders();

            return Ok(result);


        }
        [HttpPost]
        [Route("GetCatWiseItem")]
        public async Task<IActionResult> GetCatWiseItem([FromBody] int id)
        {

            var result = await _service.catwiseItems(id);

            return Ok(new { res = result });


        }
        [HttpPost]
        [Route("GetStoreProduct")]
        public async Task<IActionResult> GetStoreProd([FromBody] int id)
        {

            var result = await _service.GetStoreProduct(id);

            return Ok(new { res = result });


        }
        [HttpPost]
        [Route("SearchProduct")]
        public async Task<IActionResult> SearchProduct([FromBody] string productname)
        {

            var result = await _service.SearchProduct(productname);

            return Ok(result);


        }
        //[HttpPost]
        //[Route("GetCouponDetails")]
        //public async Task<IActionResult> GetCoupeaDetail([FromBody] string num)
        //{

        //    var identity = User.Identity as ClaimsIdentity;
        //    if (identity != null)
        //    {
        //        IEnumerable<Claim> claims = identity.Claims;
        //        var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
        //        if (name != null)
        //        {
                    
        //            var res = await _couponService.CheckCoupan(num,Convert.ToInt32(name));

        //            return Ok(new { res = res });
        //        }
        //        return Unauthorized();
        //    }
        //    return Unauthorized();
            
        //}
    }
}
