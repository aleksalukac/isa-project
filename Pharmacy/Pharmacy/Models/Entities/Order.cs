using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pharmacy.Models.Entities
{
    //razervation class
    [Table("tbOrders")]
    public class Order
    {
        public long Id { get; set; }
        public DrugAndQuantities DrugAndQuantities { get; set; }
        public string UserId { get; set; }
        public double Cost { get; set; }
        public DateTime TimeOfTransaction { get; set; }
        public bool TransactionComplete { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
