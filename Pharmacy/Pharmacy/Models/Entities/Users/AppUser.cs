﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Entities.Users
{
    [Table("tbAppUsers")]
    public class AppUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public string Adress { get; set; }
        [PersonalData]
        public string Country { get; set; }
        [PersonalData]
        public string City { get; set; }
        [PersonalData]
        public int Penalty { get; set; }
        public float AvrageScore { get; set; }
        public List<Appointment> AppointmentsForUser { get; set; }
        public List<Appointment> AppointmentsForMedical { get; set; }
        public List<AbsenceRequest> AbsenceRequests { get; internal set; }
    }
}
