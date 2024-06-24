using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SubKuchV2.DTO_s;
using SubKuchV2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.Services.SliderService
{
    public class SliderService:ISliderService
    {
        public static IWebHostEnvironment _environment;
        private readonly SubKuchDbContext _dbContext;
        public SliderService(SubKuchDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }
        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
        public async Task<string> AddSlider(SliderDto dto)
        {
           
                var filename1 = "";
                Random rnd = new Random();
                var rn = rnd.Next(111, 999);

                if (dto.ImagePath != null)
                {
                    var ImagePath1 = rn + RemoveWhitespace(dto.ImagePath.FileName);
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Content\\StoreSlider\\" + ImagePath1))
                    {

                        dto.ImagePath.CopyTo(fileStream);
                        var pathh = Path.Combine(_environment.WebRootPath, "\\Content\\StoreSlider\\" + dto.ImagePath.FileName);
                        filename1 = ImagePath1;

                        fileStream.Flush();


                    }

                Slider Slider = new Slider();
                Slider.Image = filename1;
                Slider.Sid =dto.BussinessStoreId;
                Slider.IsActive = true;
                    _dbContext.Sliders.Add(Slider);
                    await _dbContext.SaveChangesAsync();
                }
             


                return "Success";
            
        }

        public async Task<object> GetSliders()
        {
            var data = await _dbContext.Sliders.Where(x => x.IsActive == true).Select(x=>new {
                Slider = x,
                Store =_dbContext.Stores.Where(y=>y.Id==x.Sid).SingleOrDefault()

            }).ToListAsync();
         
            return data;
        }

        public async Task<string> RemoveSlider(int Id)
        {
            var dat = await _dbContext.Sliders.FindAsync(Id);
            dat.IsActive = !dat.IsActive;
            _dbContext.Sliders.Update(dat);
      
            await _dbContext.SaveChangesAsync();
            return "success";
        }
    }
}
