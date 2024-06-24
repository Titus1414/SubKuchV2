using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.Models.Dto
{
    public class ProductDto
    {
        public string CatName { get; set; }
        public int Id { get; set; }
        public DateTime? Date { get;set; }
        public string Product { get;set; }
        public decimal? PurchasePrice { get;set; }
        public decimal? SalePrice { get;set; }
        public string StoreName { get; set; }
        public int CatId { get;  set; }
 
        public int PrId { get;  set; }
    }
}
