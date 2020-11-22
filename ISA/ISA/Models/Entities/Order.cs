using ISA.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISA.Models.Entities
{
    public class Order : BaseEntity
    {
        public List<DrugAndQuantity> DrugAndQuantities { get; set; }
        public User User { get; set; }
    }
}
