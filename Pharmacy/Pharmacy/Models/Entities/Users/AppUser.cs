﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Address { get; set; }
        [PersonalData]
        public string Country { get; set; }
        [PersonalData]
        public string City { get; set; }
        [PersonalData]
        public int Penalty { get; set; }
        public float AverageScore { get; set; }
        [ForeignKey("Pharmacy")]
        public long PharmacyId { get; set; }
        public List<AbsenceRequest> AbsenceRequests { get; set; }
        public List<Drug> AllergicDrugs { get; set; }
        public TimeSpan WorkHoursStart { get; set; }
        public TimeSpan WorkHoursEnd { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
