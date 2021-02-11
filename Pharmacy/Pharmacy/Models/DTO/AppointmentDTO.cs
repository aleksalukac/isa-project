using Pharmacy.Models.Entities;
using Pharmacy.Models.Entities.Users;
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
        public long AppointmentId { get; set; }
        public long SelectedAppointmentId { get; set; }
        public long PharmacyId { get; set; }

        public AppointmentDTO()
        {

        }

        public AppointmentDTO(Appointment appointment, AppUser medicalExpert, AppUser patient)
            : this(appointment, medicalExpert.FirstName + " " + medicalExpert.LastName,
                            patient.FirstName + " " + patient.LastName)
        {
        }

        public AppointmentDTO(Appointment appointment, string _medicalExpertNameAndSurname, string _patientNameAndSurname)
        {
            this.AppointmentId = appointment.Id;
            this.MedicalExpertID = appointment.MedicalExpertID;
            this.PatientID = appointment.PatientID;
            this.Price = appointment.Price;
            this.Report = appointment.Report;
            this.StartDateTime = appointment.StartDateTime;
            this.Duration = appointment.Duration;
            this.PharmacyId = appointment.PhrmacyId;

            MedicalExpertNameAndSurname = _medicalExpertNameAndSurname;
            PatientNameAndSurname = _patientNameAndSurname;
        }
    }
}
