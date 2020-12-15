using ISA.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ISA.Models.Entities
{
    [Table("tbAppointments")]
    public class Appointment : BaseEntity
    {
        //public TimeSpan TimeSpan { get; set; }
        public Employee MedicalExpert { get; set; }
        public User Patient { get; set; }

        public float Price { get; set; }
        /*
        private _price;
        public float Price { get { return this.Price; }  set { th = value >= 0 ? value : 0; } }
        */
        /*
        public void SetPrice(float price)
        {
            if (price < 0)
                throw new Exception("Price cannot be negative");

            Price = price;
        }*/

        public Rating Rating { get; set; }
        public string Report { get; set; }
    }

    public enum Rating
    {
        Terrible = 1,
        Disatisfied,
        OK,
        Satisfied,
        Perfect
    }
}
