using ISA.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISA.Models.Entities
{
    public class Report : BaseEntity
    {
        public User User { get; set; }
        public string ReportText { get; set; }
    }
}
