using SubKuchV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class GetMenuDto
    {
        public Store BussinessStoreViewmodal { get; set; }

        public ProductCatagory StoreMenuCategoreyViewmodal { get; set; }

        public Product StoreItemViewmodal { get; set; }
    }
}
