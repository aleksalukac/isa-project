using ISA.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ISA.Models.Entities
{
    
    [Table("tbReports")]
    public class Report : BaseEntity
    {
        public User User { get; set; }
        public string ReportText { get; set; }

        public Report()
        {

        }
    }
}
