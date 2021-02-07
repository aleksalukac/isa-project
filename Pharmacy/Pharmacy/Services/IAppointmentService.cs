﻿using Pharmacy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public interface IAppointmentService
    {
        public Task<Appointment> GetById(long id);
        public Task<List<Appointment>> GetAll();
        public Task<List<Appointment>> GetByUser(string id);
    }
}