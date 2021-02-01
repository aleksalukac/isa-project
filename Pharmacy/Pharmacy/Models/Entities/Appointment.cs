using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pharmacy.Models.Entities
{
    [Table("tbAppointments")]
    public class Appointment
    {
        public long Id { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public AppUser MedicalExpert { get; set; }
        public AppUser Patient { get; set; }
        public float Price { get; set; }
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