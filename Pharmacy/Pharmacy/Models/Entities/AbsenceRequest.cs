using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pharmacy.Models.Entities
{
    [Table("tbAbsenceRequests")]
    public class AbsenceRequest
    {
        public long Id { get; set; }
        public AppUser Employee { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public AppUser PharmacyAdministrator { get; set; }
    }
}
