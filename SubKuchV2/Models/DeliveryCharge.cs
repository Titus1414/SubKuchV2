using System;
using System.Collections.Generic;

#nullable disable

namespace SubKuchV2.Models
{
    public partial class DeliveryCharge
    {
        public DeliveryCharge()
        {
            OrderPrices = new HashSet<OrderPrice>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? Kilomitter { get; set; }
        public decimal? Price { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<OrderPrice> OrderPrices { get; set; }
    }
}
