using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class LedgerFilterDto
    {
        public int AccountId { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
    }
}
