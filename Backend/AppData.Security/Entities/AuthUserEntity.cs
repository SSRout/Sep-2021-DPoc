using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Security.Entities
{
    public class AuthUserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        //Hashed Password
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
    }
}
