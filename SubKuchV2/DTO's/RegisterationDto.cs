using SubKuchV2.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SabKuchWeb.DTO_s
{
    public class RegisterationDto
    {
        public string User_Name { get; set; }
        public string Name { get; set; }
        public string Contact_Number { get; set; }
        public string Email { get; set; }
        public string Cnic { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime? ServiceStartTime { get; set; }
        public DateTime? ServiceEndTime { get; set; }
        public string Type { get; set; }
        public string Isverified { get; set; }
        public int Increasedpercentage { get; set; }
        public int Packageid { get; set; }
        public string Kitchen_Name { get; set; }
        public string RefferealCode { get; set; }

        public string Cook_About { get; set; }

        //public List<IFormFile> Idcardimage { get; set; }
        public IFormFile FrontIdCardImage { get; set; }
        public IFormFile BackIdCardImage { get; set; }
        public IFormFile CookProfileImage { get; set; }
    }
}
