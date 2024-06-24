using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class AddStoreMenuDto
    {
        public int Category_ID { get; set; }
        public string Category_Name { get; set; }


        public int ItemId { get; set; }

        public string ItemName { get; set; }
        public string ImageUrl { get; set; }
  
        public string Detail { get; set; }
        
        public int Price { get; set; }
        public int Discountedprice { get; set; }
        public IFormFile Image { get; set; }

    }
}
