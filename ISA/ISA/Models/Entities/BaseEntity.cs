using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ISA.Models.Entities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        //public DateTime CreatedAt { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime LastUpdatedAt { get; set; }
        //public string LastUpdatedBy { get; set; }
    }
}
