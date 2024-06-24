using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubKuchV2.Models;

namespace SubKuchV2.DTO_s
{
    public class OrderBillingDto
    {
        public Order OrderInfoVieModel { get; set; }

        public Store StoreInfoVieModel { get; set; }

        //public  consumerInfoVieModel { get; set; }

        public IEnumerable<Order> OrderdetailViewModel { get; set; }

        public OrderPrice OrderBillingViewModel { get; set; }




    }
}
