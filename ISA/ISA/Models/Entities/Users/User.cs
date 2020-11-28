using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ISA.Models.Entities.Users
{
    
    public class User : BaseEntity
    {
        public String Username { get; set; }
        public String Email { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public List<Appointment> Appointments { get; set; }



        [Required]
        public string Password
        {
            get
            {
                return this._password;
            }

            set
            {
                _password = SHA256(value);
            }
        }

        private String _password;

        public User()
        {

        }

        public User(string username, string email, string name, string surname, List<Appointment> appointments, string password)
        {
            Username = username;
            Email = email;
            Name = name;
            Surname = surname;
            Appointments = appointments;
            Password = password;
        }

        private static string SHA256(string Text)
        {
            var Crypt = new System.Security.Cryptography.SHA256Managed();
            var Hash = new System.Text.StringBuilder();
            byte[] Crypto = Crypt.ComputeHash(Encoding.UTF8.GetBytes(Text));
            foreach (byte TheByte in Crypto)
            {
                Hash.Append(TheByte.ToString("x2"));
            }
            return Hash.ToString();
        }


    }
}
