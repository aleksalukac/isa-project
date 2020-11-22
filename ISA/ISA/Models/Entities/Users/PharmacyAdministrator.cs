using System.Collections.Generic;

namespace ISA.Models.Entities.Users
{
    public class PharmacyAdministrator : Employee
    {
        public List<AbsenceRequest> AbsenceRequests { get; set; }
    }
}
