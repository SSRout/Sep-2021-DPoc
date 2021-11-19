using AppData.Security.Models;
using System;

namespace AppData.Security.Services
{
    public interface ISecurityService
    {
        JwtToken GenerateToken(string username, string password);
    }
}
