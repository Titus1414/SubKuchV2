using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class SliderDto
    {
    

        public IFormFile ImagePath { get; set; }

        public int? BussinessStoreId { get; set; }

    }
}
