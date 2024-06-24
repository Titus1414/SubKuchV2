using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class UpdateBuisCategoryDto
    {
        public int BussinessCategoreyId { get; set; }
        public string CatName { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public IFormFile CatImage { get; set; }

    }
}
