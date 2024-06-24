using SubKuchV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class RiderOrderDto
    {
        public Rider RiderOrderViewmodal { get; set; }
        public Rider RidertViewmodal { get; set; }
        //public Consumer ConsumerViewmodal { get; set; }
        public Order OrderViewmodal { get; set; }
    }
}
