using System;
using System.Collections.Generic;

#nullable disable

namespace SubKuchV2.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItmes = new HashSet<OrderItme>();
            Prices = new HashSet<Price>();
        }

        public int Id { get; set; }
        public int PcId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Date { get; set; }

        public virtual ProductCatagory Pc { get; set; }
        public virtual ICollection<OrderItme> OrderItmes { get; set; }
        public virtual ICollection<Price> Prices { get; set; }
    }
}
