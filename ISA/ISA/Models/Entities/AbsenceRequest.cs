using ISA.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISA.Models.Entities
{
    public class AbsenceRequest : BaseEntity
    {
        public Employee Employee { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public PharmacyAdministrator PharmacyAdministrator { get; set; }
    }
}
