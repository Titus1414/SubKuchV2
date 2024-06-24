using System;
using System.Collections.Generic;

#nullable disable

namespace SubKuchV2.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItmes = new HashSet<OrderItme>();
            OrderPrices = new HashSet<OrderPrice>();
            OrderStatuses = new HashSet<OrderStatus>();
        }

        public int Id { get; set; }
        public int? Sid { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string Location { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<OrderItme> OrderItmes { get; set; }
        public virtual ICollection<OrderPrice> OrderPrices { get; set; }
        public virtual ICollection<OrderStatus> OrderStatuses { get; set; }
    }
}
