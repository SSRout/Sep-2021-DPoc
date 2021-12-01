using AppData.Security.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Security.Services
{
    public class SecurityService : ISecurityService
    {
        public SecurityService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }

        public JwtToken GenerateToken(string username, string password)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtConfig:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer:Configuration["JwtConfig:Issuer"],
                audience:Configuration["JwtConfig:Audiance"],
                claims:null,
                expires:DateTime.Now.AddMinutes(120),
                signingCredentials:credentials);

            return new JwtToken
            {
                Jwt = new JwtSecurityTokenHandler().WriteToken(token),
                Message = "OK"
            };
        }
    }
}
