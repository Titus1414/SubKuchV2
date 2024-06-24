using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class PaymentDto
    {
        public string StoreName { get; set; }
        public int? Payment { get; set; }
        public int? PercentagePayment { get; set; }
        
    }
}
