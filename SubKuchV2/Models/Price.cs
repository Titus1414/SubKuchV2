using System;
using System.Collections.Generic;

#nullable disable

namespace SubKuchV2.Models
{
    public partial class Price
    {
        public int Id { get; set; }
        public int? PrId { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? PurchasePrice { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsActive { get; set; }

        public virtual Product Pr { get; set; }
    }
}
