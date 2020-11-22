using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISA.Models.Entities.Users
{
    public class MedicalExpert : Employee
    {
        public string MedicalLicenceId { get; set; }
        public List<Appointment> Appoitments { get; set; }
    }
}
