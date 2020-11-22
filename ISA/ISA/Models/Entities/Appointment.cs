using ISA.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISA.Models.Entities
{
    public class Appointment : BaseEntity
    {
        public TimeSpan TimeSpan { get; set; }
        public MedicalExpert MedicalExpert { get; set; }
        public User Patient { get; set; }
        public float Price { get; private set; }

        public void SetPrice(float price)
        {
            if (price < 0)
                throw new Exception("Price cannot be negative");

            Price = price;
        }

        public Rating Rating { get; set; }
        public string Report { get; set; }
    }

    public enum Rating
    {
        VeryDisatisfied = 1,
        Disatisfied,
        OK,
        Satisfied,
        VerySatisfied
    }
}
