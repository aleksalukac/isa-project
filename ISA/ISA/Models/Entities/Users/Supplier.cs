using System;
using System.Collections.Generic;
using System.Text;

namespace ISA.Models.Entities.Users
{
    public class Supplier : User
    {
        public List<SupplyOffer> SupplyOffers { get; set; }
    }
}
