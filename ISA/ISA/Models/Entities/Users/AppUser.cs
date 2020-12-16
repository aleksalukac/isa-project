using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISA.Models.Entities.Users
{
    public class AppUser : IdentityUser
    {
        public List<Appointment> Appointments { get; set; }
    }
}
