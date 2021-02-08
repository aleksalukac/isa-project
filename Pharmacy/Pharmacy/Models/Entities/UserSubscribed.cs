using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
