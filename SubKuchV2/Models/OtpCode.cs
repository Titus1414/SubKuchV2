using System;
using System.Collections.Generic;

#nullable disable

namespace SubKuchV2.Models
{
    public partial class OtpCode
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Code { get; set; }
        public DateTime? Date { get; set; }
        public bool? Status { get; set; }
        public string Validity { get; set; }
    }
}
