using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pharmacy.Models.Entities
{

    [Table("tbDrugAndQuantities")]
    public class DrugAndQuantities
    {
        public long Id { get; set; }
        public Drug Drug { get; set; }
        [ForeignKey("Pharmacy")]
        public long PharmacyId { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
