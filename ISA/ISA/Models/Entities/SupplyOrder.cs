using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ISA.Models.Entities
{
    [Table("tbSupplyOrders")]

    public class SupplyOrder : BaseEntity
    {
        public long Id { get; set; }

        public List<SupplyOffer> SupplyOffers { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public List<DrugAndQuantity> Order { get; set; }
    }

    public class DrugAndQuantity
    {
        public long Id { get; set; }

        public Drug Drug { get; set; }
        public uint Quantity { get; set; }

        //public SupplyOrder SupplyOrder;
    }
}
