using System;
using System.Collections.Generic;

#nullable disable

namespace SubKuchV2.Models
{
    public partial class Store
    {
        public Store()
        {
            ProductCatagories = new HashSet<ProductCatagory>();
        }

        public int Id { get; set; }
        public int? Bid { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
        public int? Persentage { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsOnline { get; set; }

        public virtual Business BidNavigation { get; set; }
        public virtual ICollection<ProductCatagory> ProductCatagories { get; set; }
    }
}
