using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class UpdateProductDto
    {
        public int ItemId { get; set; }
        public int MenuCategory_Id { get; set; }
    
        public string ItemName { get; set; }
        public IFormFile ImageUrl { get; set; }

        public string Detail { get; set; }

        public int Price { get; set; }
        public int Discountedprice { get; set; }
    }
}
