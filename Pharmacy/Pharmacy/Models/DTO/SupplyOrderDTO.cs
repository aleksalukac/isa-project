using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.DTO
{
    public class SupplyOrderDTO
    {
        public DrugDTO Drug { get; set; }
        public DateTime DeleveryDate { get; set; }
        public int ExtraQuantity { get; set; }
    }
}
