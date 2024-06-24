using System;
using System.Collections.Generic;

#nullable disable

namespace SubKuchV2.Models
{
    public partial class Business
    {
        public Business()
        {
            Stores = new HashSet<Store>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Date { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}
