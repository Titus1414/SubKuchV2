using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class UpdatestoreDto
    {
        public int BussinessStoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreLocation { get; set; }
        public int? Percentage { get; set; }
      
        public IFormFile ImagePath { get; set; }
        public int BussinessCategoreyId { get; set; }
        public int Rating { get; set; }
    }
}
