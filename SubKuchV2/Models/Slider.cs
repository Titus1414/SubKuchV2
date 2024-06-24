using System;
using System.Collections.Generic;

#nullable disable

namespace SubKuchV2.Models
{
    public partial class Slider
    {
        public int Id { get; set; }
        public int? Sid { get; set; }
        public string Image { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsActive { get; set; }
    }
}
