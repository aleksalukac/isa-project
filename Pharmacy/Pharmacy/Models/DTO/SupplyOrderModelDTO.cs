using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmacy.Models.Entities;

namespace Pharmacy.Models.DTO
{
    public class SupplyOrderModelDTO
    {
        public SupplyOrderDTO SupplyOrder { get; set; }
        public List<SupplyItemDTO> SupplyItems { get; set; }
        public DateTime DateExpired { get; set; }

        public SupplyOrderModelDTO()
        {

        }
    }
}
