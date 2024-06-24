using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class LedgerDto
    {
        public int AccountID { get; set; }
        public float DebitAmount { get; set; }
        public float CreditAmount { get; set; }
        public float Balance { get; set; }
        public string Particular { get; set; }
    }
}
