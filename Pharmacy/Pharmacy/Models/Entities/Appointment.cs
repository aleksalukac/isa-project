using Newtonsoft.Json;
using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pharmacy.Models.Entities
{
    [Table("tbAppointments")]
    public class Appointment
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [ForeignKey("AppUser")]
        public string MedicalExpertID { get; set; }

        [ForeignKey("AppUser")]
        public string PatientID { get; set; }
        public float Price { get; set; }
        public string Report { get; set; }
        public DateTime StartDateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public AppointmentType Type { get; set; }
        public List<Drug> PrescribedDrugs { get; set; }
        public string PrescriptionDuration { get; set; }

        [ForeignKey("Pharmacy")]
        public long PhrmacyId { get; set; }

        public void SetPrescriptionDuration(List<int> durations)
        {
            PrescriptionDuration = JsonConvert.SerializeObject(durations);
        }

        public List<int> GetPrescriptionDuration()
        {
            return (List<int>)JsonConvert.DeserializeObject(PrescriptionDuration);
        }
    }
    public enum AppointmentType
    {
        Exam,
        Counseling
    }
}
