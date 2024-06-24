using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class AddStoreDto
    {
        public string CatName { get; set; }
        public string Type { get; set; }
        public IFormFile CatImage { get; set; }

        public string StoreType { get; set; }
        public string StoreName { get; set; }
        public string StoreLocation { get; set; }
        public int? Percentage { get; set; }
        public IFormFile ImagePath { get; set; }
        public int BuisCatID { get; set; }
    }
}
