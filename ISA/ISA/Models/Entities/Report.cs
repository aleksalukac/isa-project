using ISA.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ISA.Models.Entities
{
    
    [Table("tbReport")]
    public class Report
    {
        // public User User { get; set; }
        [Key]
        public long? Id { get; set; }
        public string ReportText { get; set; }

        public Report()
        {

        }
    }
}
