using System;
using System.Collections.Generic;

#nullable disable

namespace SubKuchV2.Models
{
    public partial class OrderStatus
    {
        public int Id { get; set; }
        public int? Oid { get; set; }
        public int? Rid { get; set; }
        public string Status { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsActive { get; set; }

        public virtual Order OidNavigation { get; set; }
        public virtual Rider RidNavigation { get; set; }
    }
}
