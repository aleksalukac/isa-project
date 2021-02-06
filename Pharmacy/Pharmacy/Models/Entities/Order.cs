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
        public bool Reserved { get; set; }
    }
}
