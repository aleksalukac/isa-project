using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Models.Entities
{
    [Table("tbUserSubscribers")]
    public class UserSubscribed
    {
        public long Id { get; set; }

        [ForeignKey("AppUser")]
        public string UserId { get; set; }

        [ForeignKey("Pharmacy")]
        public long PharmacyId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
