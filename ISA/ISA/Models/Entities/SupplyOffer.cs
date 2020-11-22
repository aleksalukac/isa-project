using ISA.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISA.Models.Entities
{
    public class SupplyOffer : BaseEntity
    {
        public Supplier Supplier { get; set; }
        public SupplyOrder SupplyOrder { get; set; }
        public float Offer { get; private set; }

        public void SetOffer(float offer)
        {
            if (offer < 0)
                throw new Exception("Offer cannot be negative");

            Offer = offer;
        }
    }
}
