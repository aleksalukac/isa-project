using Pharmacy.Models.Entities;
using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.DTO
{
    public class AppointmentExamDTO : AppointmentDTO
    {
        public AppointmentExamDTO()
        {

        }

        public AppointmentExamDTO(Appointment appointment, AppUser medicalExpert, AppUser patient)
               : base(appointment, medicalExpert, patient)
        {
        }

        public long PrescribedDrug { get; set; }
        public long PrescriptionLength { get; set; }
    }
}
