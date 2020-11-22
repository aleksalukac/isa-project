using ISA.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISA.Models.Entities
{
    public class Pharmacy : BaseEntity
    {
        public String Address { get; set; }
        public PharmacyAdministrator Administrator { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Drug> Drugs { get; set; }
        public List<SupplyOrder> SupplyOrders { get; set; }
    }
}
