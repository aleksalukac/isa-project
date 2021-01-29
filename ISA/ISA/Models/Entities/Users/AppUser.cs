using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISA.Models.Entities.Users
{
    [Table("tbAppUsers")]
    public class AppUser : IdentityUser
    {
        public string Password { get; set; }
        
        public List<Appointment> Appointments { get; set; }
        //public AppUser(string Password): base()
        //{
        //    this.Password = Password;
        //}
    }
}
