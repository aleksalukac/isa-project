﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ISA.Models.Entities.Users
{
    public class Dermatologist : MedicalExpert
    {
        public List<Appointment> Appoitments { get; set; }
    }
}
