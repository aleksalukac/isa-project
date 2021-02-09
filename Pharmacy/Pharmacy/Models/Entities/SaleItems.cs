using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pharmacy.Models.Entities
{

    [Table("tbSaleItems")]
    public class SaleItems
    {
        public long Id { get; set; }

        [ForeignKey("tbDrugAndQuantities")]
        public long DrugAndQuantitiesId { get; set; }
        public double BeforePrice { get; set; }
        public DateTime EndTime { get; set; }

    }
}
