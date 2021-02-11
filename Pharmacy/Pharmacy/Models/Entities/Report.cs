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
        public AppUser Employee { get; set; }
        public Pharmacy Pharmacy { get; set; }  
        public string ReportText { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
