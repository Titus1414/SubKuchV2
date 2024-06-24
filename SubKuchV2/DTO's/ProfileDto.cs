using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubKuchV2.Models;

namespace SabKuchWeb.DTO_s
{
    public class ProfileDto
    {
    
        public IFormFile ProfileImage { get; set; }
    }
}
