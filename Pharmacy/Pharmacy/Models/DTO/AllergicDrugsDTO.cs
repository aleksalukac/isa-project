using Pharmacy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.DTO
{
    public class AllergicDrugsDTO
    {
        public long DrugId { get; set; }
        public string DrugName { get; set; }
        public bool Allergic { get; set; }
    }
}
