using System;
using System.Collections.Generic;

#nullable disable

namespace SubKuchV2.Models
{
    public partial class OrderPrice
    {
        public int Id { get; set; }
        public int? Oid { get; set; }
        public int? DcId { get; set; }
        public int? CcId { get; set; }
        public decimal? DiscountPrice { get; set; }
        public DateTime? Date { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool? IsActive { get; set; }

        public virtual CouponCode Cc { get; set; }
        public virtual DeliveryCharge Dc { get; set; }
        public virtual Order OidNavigation { get; set; }
    }
}
