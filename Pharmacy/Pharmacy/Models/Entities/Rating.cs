using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pharmacy.Models.Entities
{
    
    [Table("tbRating")]
    public class Rating
    {
        public long Id { get; set; }
        [Required]
        public AppUser User { get; set; }
        public AppUser Employee { get; set; }
        public Drug Drug { get; set; }
        public Pharmacy Pharmacy { get; set; }
        [Required]
        public int Score { get; set; }
    }
}
