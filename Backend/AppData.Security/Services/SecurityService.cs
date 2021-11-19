using AppData.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Security.Services
{
    public class SecurityService : ISecurityService
    {
        public JwtToken GenerateToken(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
