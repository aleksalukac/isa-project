using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pharmacy.Models.Entities
{
    [Table("tbOrders")]
    public class Order
    {
        public long Id { get; set; }
        public List<DrugAndQuantities> DrugAndQuantities { get; set; }
        public double Cost { get; set; }
        public DateTime TimeOfTransaction { get; set; }
        public bool Reserved { get; set; }
        public bool Sold { get; set; }
        public bool ReSupply { get; set; }
    }
}
