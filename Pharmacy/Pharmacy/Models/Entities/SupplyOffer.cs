using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pharmacy.Models.Entities
{
    [Table("tbSupplyOffers")]
    public class SupplyOffer
    {
        public long Id { get; set; }
        public AppUser Supplier { get; set; }
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
