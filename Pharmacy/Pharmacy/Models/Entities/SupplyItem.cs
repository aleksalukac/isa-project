using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Entities
{
    [Table("tbSupplyItems")]
    public class SupplyItem
    {
        public SupplyItem()
        {

        }

        public SupplyItem(long id, long drugId, long supplyOrderId, int extraQuantity)
        {
            Id = id;
            DrugId = drugId;
            SupplyOrderId = supplyOrderId;
            ExtraQuantity = extraQuantity;
        }

        public long Id { get; set; }

        [ForeignKey("tbDrugAndQuantities")]
        public long DrugId { get; set; }

        [ForeignKey("tbSupplyOrders")]
        public long SupplyOrderId { get; set; }


        public int ExtraQuantity { get; set; }

        
    }
}
