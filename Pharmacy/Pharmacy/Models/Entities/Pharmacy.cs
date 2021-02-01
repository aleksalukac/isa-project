using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pharmacy.Models.Entities
{
    [Table("tbPharmacys")]
    public class Pharmacy
    {
        public long Id { get; set; }
        public string Address { get; set; }
        [ForeignKey("AppUser")]
        public string AdminUserID { get; set; }
        //public AppUser Administrator { get; set; }
        public List<AppUser> Employees { get; set; }
        public List<Drug> Drugs { get; set; }
        public List<SupplyOrder> SupplyOrders { get; set; }
    }
}
