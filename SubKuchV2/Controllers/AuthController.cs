using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NuBolet.Models.CommonClasses;
using SubKuchV2.Models;
using SubKuchV2.Models.Dto;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SabKuchWeb.DTO_s;
using SabKuchWeb.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Imaging;
using SubKuchV2.DTO_s;

namespace SubKuchV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthservice _service;
        private readonly SubKuchDbContext _dbContext;
        public readonly IConfiguration _config;
        public AuthController(IAuthservice service, SubKuchDbContext dbContext, IConfiguration config)
        {
            _service = service;
            _dbContext = dbContext;
            _config = config;
        }


        [HttpPost]
        [Route("Otp")]
        public async Task<IActionResult> OTP([FromBody] string number)
        {
            try
            {
                var result = await _service.OTP(number);
                if (result != null)
                {
                    return Ok("ok");

                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

                throw;
            }
        }
        

        [HttpPost]
        [Route("OTPCode")]
        public async Task<IActionResult> LoginCode([FromBody] LoginDto Dto)
        {
            try
            {
                var otpverification = await _service.NumberAuthentication(Dto);
                if (otpverification != null)
                {
                    _dbContext.OtpCodes.Remove(otpverification);
                    await _dbContext.SaveChangesAsync();
                    var user = await _service.UserExist(Dto.Username);
                    if (user != null)
                    {



                        var claims = new[]
                   {
                          new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                          new Claim(ClaimTypes.Name, user.UserName),
                          new Claim(ClaimTypes.Role,user.Type),

                          new Claim("ID",user.UserId.ToString())

                   };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.
                            GetBytes(_config.GetSection("AppSettings:Token").Value));

                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(claims),
                            Expires = DateTime.Now.AddDays(5),
                            SigningCredentials = creds
                        };

                        var tokenHandler = new JwtSecurityTokenHandler();

                        var token = tokenHandler.CreateToken(tokenDescriptor);

                        return Ok(new
                        {
                            token = tokenHandler.WriteToken(token)
                        });

                    }
                    return Ok(new { token = "OTPV" });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        // POST: api/Auth
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterationDto Dto)
        {
            try
            {


                var result = await _service.Registration(Dto);
                if (result != null)
                {
                    var claims = new[]
                     {
                          new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()),
                          new Claim(ClaimTypes.Name, result.UserName),
                          new Claim(ClaimTypes.Role,result.Type),

                          new Claim("ID",result.UserId.ToString())
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.
                        GetBytes(_config.GetSection("AppSettings:Token").Value));

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.Now.AddDays(5),
                        SigningCredentials = creds
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();

                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return Ok(new
                    {
                        token = tokenHandler.WriteToken(token)
                    });

                }
                return BadRequest(new { res = result });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("RiderLogin")]
        public async Task<IActionResult> RiderLogin([FromBody] RiderLoginDto Dto)
        {
            try
            {

                var user = await _service.RiderLogin(Dto);
                if (user != null)
                {

                    var claims = new[]
               {
                          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                          new Claim(ClaimTypes.Name, user.Id.ToString()),
                          new Claim("ID",user.Id.ToString())

                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.
                        GetBytes(_config.GetSection("AppSettings:Token").Value));

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.Now.AddDays(5),
                        SigningCredentials = creds
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();

                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return Ok(new
                    {
                        token = tokenHandler.WriteToken(token)
                    });

                }
                return Unauthorized(); ;

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
