using ISA.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ISA.Models.Entities
{
    [Table("tbAbsenceRequests")]
    public class AbsenceRequest : BaseEntity
    {
        public Employee Employee { get; set; }
        //public TimeSpan TimeSpan { get; set; }
        public Employee PharmacyAdministrator { get; set; }
    }
}
