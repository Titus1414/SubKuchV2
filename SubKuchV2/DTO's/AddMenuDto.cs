using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SubKuchV2.DTO_s
{
    public class AddMenuDto
    {
 
     
        public int Category_ID { get; set; }
        public string Category_Name { get; set; }

       
        public int DishId { get; set; }
     
        public string Meal_name { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
        public string DishDetail { get; set; }
     
        public int Price { get; set; }
     
        public IFormFile  DishImage { get; set; }
    
    }
}
