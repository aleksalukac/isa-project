using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pharmacy.Models.Entities
{
    [Table("tbSupplyOrders")]

    public class SupplyOrder
    {
        public long Id { get; set; }
        public List<SupplyOffer> SupplyOffers { get; set; }
        [ForeignKey("Pharmacy")]
        public long PharmacyId { get; set; }
        public DateTime DeleveryDate { get; set; }
        [ForeignKey("tbDrugAndQuantities")]
        public long DrugId { get; set; }
        public int ExtraQuantity { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }

}
