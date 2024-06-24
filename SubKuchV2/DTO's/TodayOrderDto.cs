using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class TodayOrderDto
    {
        public int orderid { get; set; }
        public int orderCode { get; set; }
        public string ConsumerName { get; set; }
        public string ConsumerPhone { get; set; }
        public string citchenName { get; set; }
        public string citchenPhone { get; set; }
        public DateTime datee { get; set; }
        public string status { get; set; }
       
        public List<Dishname> Orderdetails { get; set; }
        public int orderBilling { get; set; }

    }
}
