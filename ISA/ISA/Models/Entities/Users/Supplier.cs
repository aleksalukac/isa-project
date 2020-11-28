using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ISA.Models.Entities.Users
{
    [Table("tbSuppliers")]
    public class Supplier : User
    {
        Supplier()
        {

        }

        public Supplier(User user, List<SupplyOffer> supplyOffers) :
        base(user.Username, user.Email, user.Name, user.Surname, user.Appointments, user.Password)
        {
            SupplyOffers = supplyOffers;
        }
        public List<SupplyOffer> SupplyOffers { get; set; }
    }
}
