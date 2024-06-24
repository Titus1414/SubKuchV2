using System;
using System.Collections.Generic;

#nullable disable

namespace SubKuchV2.Models
{
    public partial class OrderItme
    {
        public int Id { get; set; }
        public int? PrId { get; set; }
        public int? Quantity { get; set; }
        public int? Oid { get; set; }
        public bool? IsActive { get; set; }

        public virtual Order OidNavigation { get; set; }
        public virtual Product Pr { get; set; }
    }
}
