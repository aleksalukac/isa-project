using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pharmacy.Models.Entities
{
    
    [Table("tbReports")]
    public class Report
    {
        public long Id { get; set; }
        public AppUser User { get; set; }
        public string ReportText { get; set; }

        public Report()
        {

        }
    }
}
