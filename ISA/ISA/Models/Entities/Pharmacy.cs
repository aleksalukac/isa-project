using ISA.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ISA.Models.Entities
{
    [Table("tbPharmacys")]
    public class Pharmacy : BaseEntity
    {
        public String Address { get; set; }
        
        //public Employee Administrator { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Drug> Drugs { get; set; }
        public List<SupplyOrder> SupplyOrders { get; set; }
    }
}
