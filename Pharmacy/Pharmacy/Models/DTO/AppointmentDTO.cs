using Pharmacy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.DTO
{
    public class AppointmentDTO : Appointment
    {
        public string MedicalExpertNameAndSurname { get; set; }
        public string PatientNameAndSurname { get; set; }

        public AppointmentDTO(Appointment appointment, string _medicalExpertNameAndSurname, string _patientNameAndSurname)
        {
            this.MedicalExpertID = appointment.MedicalExpertID;
            this.PatientID = appointment.PatientID;
            this.Price = appointment.Price;
            this.Report = appointment.Report;
            this.StartDateTime = appointment.StartDateTime;
            this.Duration = appointment.Duration;

            MedicalExpertNameAndSurname = _medicalExpertNameAndSurname;
            PatientNameAndSurname = _patientNameAndSurname;
        }
    }
}
