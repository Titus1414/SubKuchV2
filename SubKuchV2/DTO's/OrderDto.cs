using SubKuchV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class OrderDto
    {
        public Order Order { get; set; }
        public List<Order> OrderItems { get; set; }
        public OrderPrice OrderBilling { get; set; }
      
        public CouponCode Coupen { get; set; }
    }
}
