using AppData.Security.IServices;
using AppData.Security.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace AppData.Security.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IAuthUserService _authUserService;

        public SecurityService(
            IConfiguration configuration,
            IAuthUserService authUserService)
        {
            _authUserService = authUserService;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public JwtToken GenerateToken(string username, string password)
        {
            var user = _authUserService.GetUser(username);
            //Validate User - Generate
            if (Authenticate(password, user))
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtConfig:Secret"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(Configuration["JwtConfig:Issuer"],
                    Configuration["JwtConfig:Audience"],
                    null,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: credentials);
                return new JwtToken
                {
                    Jwt = new JwtSecurityTokenHandler().WriteToken(token),
                    Message = "Ok"
                };
            }

            throw new AuthenticationException("Invalid User or Password not correct");

            //return new JwtToken
            //{
            //    Message = "Invalid User or Password not correct"
            //};
        }
        private bool Authenticate(string plainTextPassword, AuthUser user)
        {
            if (user == null || user.HashedPassword.Length <= 0 || user.Salt.Length <= 0)
                return false;

            var hashedPasswordFromPlain = HashedPassword(plainTextPassword, user.Salt);
            return user.HashedPassword.Equals(hashedPasswordFromPlain);
        }

        public string HashedPassword(string plainTextPassword, byte[] userSalt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: plainTextPassword,
                salt: userSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }

        public AuthUser GenerateNewUser(string username,string passwprd)
        {
            var defaultPassword = passwprd??"123456";
            var salt = GenerateSalt();
            var hashedPwd= HashedPassword(defaultPassword, salt);

            AuthUser user=_authUserService.Create(new AuthUser
            {
                UserName=username,
                HashedPassword = hashedPwd,
                Salt=salt
            });

            return user;
        }

        public byte[] GenerateSalt()
        {
            var salt=new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return salt;
        }
    }
}
