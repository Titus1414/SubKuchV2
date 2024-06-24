using SubKuchV2.DTO_s;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.Extensions.Logging;
using NuBolet.Models.CommonClasses;
using SubKuchV2.Models;
using SubKuchV2.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SabKuchWeb.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Imaging;
using SabKuchWeb.DTO_s;

namespace SabKuchWeb.Services.AuthService
{
    public class AuthService : IAuthservice
    {
        public readonly SubKuchDbContext _dbContext;
        public static IWebHostEnvironment _environment;

        public AuthService(SubKuchDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _environment = webHostEnvironment;

        }
        public async Task<OtpCode> NumberAuthentication(LoginDto dto)
        {
            var data = await _dbContext.OtpCodes.Where(x => x.Phone == dto.Username && x.Code == dto.Code).FirstOrDefaultAsync();
            if (data != null)
            {
                return data;

            }
            return null;
        }


        public async Task<Userr> Registration(RegisterationDto user)
        {
            try
            {
                Userr UserDataToStore = new Userr
                {
                    Name = user.Name,
                    UserName = user.Contact_Number,
                    Address = user.Address,
                    ContactNumber = user.Contact_Number,
                    Email = user.Email,
                    Cnic = user.Cnic,
                    Gender = user.Gender,

                    Type = user.Type,
                    Status = true,
                    DateTime = DateTime.Now,
                };
                await _dbContext.Userrs.AddAsync(UserDataToStore);
                await _dbContext.SaveChangesAsync();
                var Userr = _dbContext.Userrs.Max(p => p.UserId);
                Userr userdata = await _dbContext.Userrs.Where(x => x.UserId == Userr).SingleOrDefaultAsync();
                var otp = _dbContext.OtpCodes.Where(x => x.Phone == user.Contact_Number).ToList();
                foreach (var item in otp)
                {
                    _dbContext.OtpCodes.Remove(item);
                    await _dbContext.SaveChangesAsync();
                }

                //Consumer consumer = new Consumer()
                //{
                //    Consumer_Name = user.Name,
                //    Mobile_Number = user.Contact_Number,
                //    Address = user.Address,
                //    DateTime = DateTime.Now,
                //    Email = user.Email,
                //    Gender = user.Gender,
                //    Status = true,
                //    User_id = Userr,
                //    RefferedBy = user.RefferealCode
                //};
                //await _dbContext.Consumers.AddAsync(consumer);
                //await _dbContext.SaveChangesAsync();
                //var consumerid = await _dbContext.Consumers.MaxAsync(x => x.Consumer_Id);

                Random rnd = new Random();
                var number = rnd.Next(100, 999);
                string str = user.Name.Substring(0, 3);

                return userdata;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
        public async Task<Userr> UserExist(string username)
        {
            var user = await _dbContext.Userrs.Where(user => user.UserName == username).AnyAsync();
            if (user)
                return await _dbContext.Userrs.Where(x => x.UserName == username).FirstOrDefaultAsync();

            return null;
        }



        public async Task<string> OTP(string number)
        {
            try
            {
                Random random = new Random();
                var randomvalue = random.Next(1000, 9999).ToString();
                var mesage = randomvalue;
                //var accountSid = "AC68ea59e8902debf6d75fe046731ed77a";
                //var authToken = "56781a2fa234257a3c21480567aa157e";
                var accountSid = "AC8f1e209d3372cf327b4152443a8bbcdb";
                var authToken = "37546db504d8be9dae340b464c835cdf";
                TwilioClient.Init(accountSid, authToken);

                var splitnum = number.Substring(0, 5);

                var message = MessageResource.Create(
               body: mesage,
               from: new Twilio.Types.PhoneNumber("+1 224 655 5058"),
               to: new Twilio.Types.PhoneNumber(number)
               );
                var check = await _dbContext.OtpCodes.AnyAsync(x => x.Phone == number);
                if (check)
                {
                    var getcodes = await _dbContext.OtpCodes.Where(x => x.Phone == number).ToListAsync();
                    _dbContext.OtpCodes.RemoveRange(getcodes);
                    await _dbContext.SaveChangesAsync();
                }
                OtpCode code = new OtpCode()
                {
                    Phone = number,
                    Code = randomvalue,
                    Validity = DateTime.Now.AddMinutes(5).ToString(),
                    Status = true,
                    Date = DateTime.Now

                };
                await _dbContext.OtpCodes.AddAsync(code);
                await _dbContext.SaveChangesAsync();
                return message.Sid;
            }
            catch (Exception ex)
            {

                throw;
            }

            

        }



        //public async Task<CustomUser> CustomUserLogin(CustomUseroginDto Dto)
        //{
        //    var user = await _dbContext.CustomUsers.FirstOrDefaultAsync(x => x.Email == Dto.Email);

        //    if (user == null)

        //        return null;

        //    if (!VerifyPasswordHash(Dto.Password, user.PasswordHash, user.PasswordSalt))
        //        return null;

        //    return user;
        //}

        //private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        //{
        //    using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        for (int i = 0; i < computedHash.Length; i++)
        //        {
        //            if (computedHash[i] != passwordHash[i]) return false;
        //        }
        //        return true;
        //    }
        //}

        //public async Task<CustomUser> CustomserRegisteration(CustomUseroginDto dto)
        //{
        //    byte[] passwordHash, passwordSalt;
        //    CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);

        //    CustomUser user = new CustomUser();
        //    user.PasswordHash = passwordHash;
        //    user.PasswordSalt = passwordSalt;
        //    user.Email = dto.Email;
        //    user.Name = dto.UserName;

        //    await _dbContext.CustomUsers.AddAsync(user);

        //    await _dbContext.SaveChangesAsync();

        //    return user;
        //}

        //private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using (var hmac = new System.Security.Cryptography.HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }

        //}

        public async Task<Rider> RiderLogin(RiderLoginDto Dto)
        {
            var login = await _dbContext.Riders.Where(x => x.Phone == Dto.Phone && x.Password == Dto.Password && x.IsActive == true).SingleOrDefaultAsync();
            if (login != null)
            {
                // var data = await _dbContext.Riders.Where(x => x.RiderPhone == Dto.Phone && x.Password == Dto.Password).SingleOrDefaultAsync();
                return login;
            }
            return null;
        }

        public async Task<string> Offline(int id, bool Isoffline)
        {
            var dt = _dbContext.Riders.Where(a => a.Id == id).FirstOrDefault();

            dt.IsOnline = Isoffline;
            _dbContext.Riders.Update(dt);
            _dbContext.SaveChanges();

            return "Success";

        }

        public Task<bool> IsOnline(int id)
        {
            throw new NotImplementedException();
        }
    }
}
