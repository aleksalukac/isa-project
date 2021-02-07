using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Entities
{
    [Table("PatientAllergies")]
    public class PatientAllergy
    {
        public long Id { get; set; }

        [ForeignKey("AppUser")]
        public string PatientID { get; set; }

        [ForeignKey("Drug")]
        public long DrugID { get; set; }
    }
}
