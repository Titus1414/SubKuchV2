using System;
using System.Collections.Generic;

#nullable disable

namespace SubKuchV2.Models
{
    public partial class CouponCode
    {
        public CouponCode()
        {
            OrderPrices = new HashSet<OrderPrice>();
        }

        public int Id { get; set; }
        public string MinimumOrder { get; set; }
        public string Code { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsActive { get; set; }
        public bool? CurrentStatus { get; set; }
        public DateTime? ValidityDate { get; set; }

        public virtual ICollection<OrderPrice> OrderPrices { get; set; }
    }
}
