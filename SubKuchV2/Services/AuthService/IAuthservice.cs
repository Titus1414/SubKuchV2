using SubKuchV2.DTO_s;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubKuchV2.Models;
using SabKuchWeb.DTO_s;

namespace SabKuchWeb.Services.AuthService
{
    public  interface IAuthservice
    {
        Task<Userr> Registration(RegisterationDto user);
     
        Task<string> OTP(string number);
        Task<OtpCode> NumberAuthentication(LoginDto dto);
        Task<Userr> UserExist(string username);

        //public Task<CustomUser> CustomserRegisteration(CustomUseroginDto Dto);
        //public Task<CustomUser> CustomUserLogin(CustomUseroginDto dto);
        Task<Rider> RiderLogin(RiderLoginDto Dto);

        public Task<string> Offline ( int id, bool Isoffline);

        public Task<bool> IsOnline(int id);



    }
}
