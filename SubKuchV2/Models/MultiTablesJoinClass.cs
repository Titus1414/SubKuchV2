using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.Models
{
    public class MultiTablesJoinClass
    {
        public Order ordersList { get; set; }
        public OrderItme orderItemsList { get; set; }
        public Product productsList { get; set; }
        public OrderPrice orderPricesList { get; set; }
        public Price pricesList { get; set; }
        public OrderStatus statusList { get; set; }
        public Store storesList { get; set; }


    }
}
