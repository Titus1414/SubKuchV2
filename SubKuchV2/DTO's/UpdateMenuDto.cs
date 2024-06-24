using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.DTO_s
{
    public class UpdateMenuDto
    {
        public int DishId { get; set; }
        public int MenuCategory_Id { get; set; }
     
        public string Meal_name { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
        public string Detial { get; set; }
        public bool Availability { get; set; }
        public int Price { get; set; }
        public IFormFile  Image{ get; set; }

    }
}
