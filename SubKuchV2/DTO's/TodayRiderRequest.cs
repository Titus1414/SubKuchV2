using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class TodayRiderRequest
    {
        public int OrderId { get; set; }
        public int OrderCode { get; set; }
        public string RiderName { get; set; }
        public string RiderContact { get; set; }
        public string ridestatus { get; set; }

    }
}
