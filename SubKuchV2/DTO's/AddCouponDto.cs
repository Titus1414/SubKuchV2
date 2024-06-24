using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class AddCouponDto
    {
        public int DiscountedAmount { get; set; }
        public string CuoponCode { get; set; }
        public string MinimumOrder { get; set; }
        public bool Status { get; set; }
        public DateTime ValidityDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
