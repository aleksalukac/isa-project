using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISA.Models.Entities.Users
{
    [Table("tbAppUsers")]
    public class AppUser : IdentityUser<long>
    {
        public List<Appointment> Appointments { get; set; }
    }
}
