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
        [ForeignKey("AppUser")]
        public string EmployeeId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        [ForeignKey("AppUser")]
        public string PharmacyAdministratorId { get; set; }
        public bool Approved { get; set; }
    }
}
