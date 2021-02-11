using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.DTO
{
    public class LoginDTO
    {
        public string username { get; set; }
        public string password { get; set; }

        public LoginDTO(string Username, string Password)
        {
            username = Username;
            password = Password;
        }
    }
}
