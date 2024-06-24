using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubKuchV2.Models;

namespace SubKuchV2.DTO_s
{
    public class VenderUpdateDto
    {
     

        public int CookID { get; set; }
        public string Kitchen_Name { get; set; }

        public string User_Name { get; set; }
        public string Type { get; set; }
        public string Cook_Name { get; set; }
        public int User_id { get; set; }
      
        public string Cook_Location { get; set; }
        public string Cook_About { get; set; }
        public DateTime? ServiceStartTime { get; set; }
        public DateTime? ServiceEndTime { get; set; }
        public bool Isverified { get; set; }
        public string Cook_Email { get; set; }
        public int Increasedpercentage { get; set; }
        public string Cook_Cnic { get; set; }
        public string ProfileImagePath { get; set; }
        public string Cook_mobile { get; set; }
        public string Cook_Gender { get; set; }
        public bool Status { get; set; }
        public bool cookIsOnline { get; set; }
        public DateTime DateTime { get; set; }
        public int Packageid { get; set; }
        public IFormFile CookProfileImage { get; set; }

    }
}
