using AppData.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Security.IServices
{
    public interface IAuthUserService
    {
        AuthUser GetUser(string username);
        AuthUser Create(AuthUser authUser);
    }
}
