using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pharmacy.Models.Entities
{

    [Table("tbDermatologistPharmacy")]
    public class DermatologistPharmacy
    {
        public long Id { get; set; }

        [ForeignKey("AppUser")]
        public long UserId { get; set; }
        [ForeignKey("Pharmacy")]
        public long PharmacyId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
