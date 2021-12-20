using AppData.Security.Entities;
using AppData.Security.IRepositories;
using AppData.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Security.Reposities
{
    public class AuthUserRepository : IAuthUserRepository
    {
        private readonly AuthDbContext _ctx;

        public AuthUserRepository(AuthDbContext ctx)
        {
            _ctx = ctx;
        }

        public AuthUser Create(AuthUser authUser)
        {
           var entity= _ctx.Add(new AuthUserEntity
            {
                HashedPassword=authUser.HashedPassword,
                Salt=Convert.ToBase64String(authUser.Salt),
                Username=authUser.UserName
            }).Entity;
            _ctx.SaveChanges();
            return new AuthUser
            {
                Id = entity.Id,
                UserName = entity.Username
            };
        }

        public AuthUser FindByUsernameAndPassword(string username, string hashedPassword)
        {
            var entity = _ctx.AuthUsers
                .FirstOrDefault(user =>
                    hashedPassword.Equals(user.HashedPassword) &&
                    username.Equals(user.Username));
            if (entity == null) return null;
            return new AuthUser
            {
                Id = entity.Id,
                UserName = entity.Username
            };
        }

        public AuthUser FindUser(string username)
        {
            var entity = _ctx.AuthUsers
                .FirstOrDefault(user => username.Equals(user.Username));
            if (entity == null) return null;
            return new AuthUser
            {
                Id = entity.Id,
                UserName = entity.Username,
                HashedPassword = entity.HashedPassword,
                Salt = Convert.FromBase64String(entity.Salt)
            };
        }
    }
}
