using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pharmacy.Models.Entities
{
    //razervation class
    [Table("tbTransaction")]
    public class Transaction
    {
        public long Id { get; set; }
        public Drug Drug {get; set;}
        public double Cost { get; set; }
        public DateTime TimeOfTransaction { get; set; }
    }
}
