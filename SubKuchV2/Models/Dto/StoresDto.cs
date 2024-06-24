using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.Models.Dto
{
    public class StoresDto
    {
        public int Id { get; set; }
        public int BusinId { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public string Location { get; set; }
        public string BusinessName { get; set; }
        public int? Percentage { get; internal set; }
    }
}
