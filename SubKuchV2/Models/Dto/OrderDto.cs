using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.Models.Dto
{
    public class OrderDto
    {

        public object CustomerName { get; set; }
        public object CustomerPhone { get; set; }
        public int PrId { get;set; }
        public string Location { get;set; }
        public DateTime? Date { get; set; }
        public int Sid { get; set; }
        public int Id { get; set; }

        public string OrderStatus { get;  set; }
        public string StoreLocation { get;  set; }
        public string StoreName { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string ProductName { get; set; }
        public int? Quantity { get;  set; }
        public decimal? SalePrice { get;  set; }
        public decimal? netamount { get; set; }
        public int? percenatge { get; set; }
        public decimal? DeliverCharges { get; set; }
        public decimal? GrandAmount { get; set; }
        public decimal? CouponDiscountedPrice { get; internal set; }
        public decimal? Amount { get; internal set; }
    }
}
