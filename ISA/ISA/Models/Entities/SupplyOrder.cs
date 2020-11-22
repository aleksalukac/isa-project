using System;
using System.Collections.Generic;
using System.Text;

namespace ISA.Models.Entities
{
    public class SupplyOrder : BaseEntity
    {
        public List<SupplyOffer> SupplyOffers { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public List<DrugAndQuantity> Order { get; set; }
    }

    public class DrugAndQuantity
    {
        public Drug Drug { get; set; }
        public uint Quantity { get; set; }
    }
}
