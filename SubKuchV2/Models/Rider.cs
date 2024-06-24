using System;
using System.Collections.Generic;

#nullable disable

namespace SubKuchV2.Models
{
    public partial class Rider
    {
        public Rider()
        {
            OrderStatuses = new HashSet<OrderStatus>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Cnic { get; set; }
        public string Vehical { get; set; }
        public string ModelNumber { get; set; }
        public string Password { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; }
        public bool? IsOnline { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<OrderStatus> OrderStatuses { get; set; }
    }
}
