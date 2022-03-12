using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using wwe.Models;

namespace API_Login_II.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly WweContext _context;
        private readonly IConfiguration configuration;
        public LoginController(WweContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }
        
        [HttpPost]
        public string Login(User user)
        {
            //Validate the loggged in user
            if (UserExistsUsername(user.UserName))
            {
                if (_context.Users.Any(e => e.UserName == user.UserName) && _context.Users.Any(e => e.Password == user.Password))
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("TokenAuthentication:SecretKey").Value));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                      issuer: configuration.GetSection("TokenAuthentication:Issuer").Value,
                      null,
                      expires: DateTime.Now.AddMinutes(60),
                      claims: new List<Claim> { new Claim(ClaimTypes.Role, user.UserName == "Admin" ? "Admin" : "User") },
                      signingCredentials: credentials);

                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
            }
            return "Unauthorized";
        }
        private bool UserExistsUsername(string uname)
        {
            return _context.Users.Any(e => e.UserName == uname);
        }
    }
}
