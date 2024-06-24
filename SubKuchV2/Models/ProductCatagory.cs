using System;
using System.Collections.Generic;

#nullable disable

namespace SubKuchV2.Models
{
    public partial class ProductCatagory
    {
        public ProductCatagory()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public int? Sid { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Date { get; set; }

        public virtual Store SidNavigation { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
