using AppData.Security.IRepositories;
using AppData.Security.IServices;
using AppData.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Security.Services
{
    public class AuthUserService : IAuthUserService
    {
        private readonly IAuthUserRepository _authUserRepository;

        public AuthUserService(IAuthUserRepository authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }
        public AuthUser GetUser(string username)
        {
            return _authUserRepository.FindUser(username);
        }
    }
}
