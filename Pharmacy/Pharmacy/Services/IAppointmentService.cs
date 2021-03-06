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
        public Task<List<Appointment>> GetByMedicalExpert(string id);
        public Task<List<Appointment>> GetAllForPharmacy(long id);
        public Task<List<Appointment>> GetByPatient(string id);
        public Task<List<Appointment>> GetCurrentByMedicalExpert(string id);
        public Task<List<Appointment>> GetFreeDermatologistApp();
        public Task<List<Appointment>> GetByPatientScheduled(string id);
        public Task<List<Appointment>> GetByPatientPast(string id);
        public Task<int> Update(Appointment appointment);
        public bool Exists(long id);
        public Task<int> Remove(Appointment appointment);
        public Task<bool> Create(Appointment appointment);
        public Task<List<Appointment>> GetByMedicalExpertFree(string id);
    }
}
