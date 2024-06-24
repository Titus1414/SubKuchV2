using SubKuchV2.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubKuchV2.Services.SliderService
{
   public interface ISliderService
    {
        Task<Object> GetSliders();
        Task<string> RemoveSlider(int Id);
        Task<string> AddSlider(SliderDto dto);
    }
}
