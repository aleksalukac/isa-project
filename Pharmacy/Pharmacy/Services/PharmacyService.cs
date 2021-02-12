using Pharmacy.Data;
using Pharmacy.Models.Entities.Users;
using Pharmacy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Pharmacy.Services
{
    public class PharmacyService : IPharmacyService
    {
        private readonly ApplicationDbContext _context;

        public PharmacyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetAdmin(long pharmacyId)
        {
            Pharmacy.Models.Entities.Pharmacy pharmacy = await _context.tbPharmacys.FindAsync(pharmacyId);
            if(pharmacy == null)
            {
                return null;
            }
            return pharmacy.AdminUserID;
        }

        public async Task<int> Update(Pharmacy.Models.Entities.Pharmacy pharmacy)
        {
            _context.Update(pharmacy);

            return await _context.SaveChangesAsync();
        }

        public bool Exists(long id)
        {
            return _context.tbPharmacys.Any(e => e.Id == id);
        }

        public async Task<Pharmacy.Models.Entities.Pharmacy> GetById(long id)
        {
            return await _context.tbPharmacys.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Models.Entities.Pharmacy>> GetAllFiltered(string searchString, string filter, string sort)
        {

            var pharmacies = new List<Models.Entities.Pharmacy>();
            if (sort != null)
            {
                if (sort == "Score")
                {
                    pharmacies = await _context.tbPharmacys.OrderBy(x => x.AverageScore).ToListAsync();
                }
                else if (sort == "Name")
                {
                    pharmacies = await _context.tbPharmacys.OrderBy(x => x.Name).ToListAsync();
                }
                else if (sort == "Adress")
                {
                    pharmacies = await _context.tbPharmacys.OrderBy(x => x.Address).ToListAsync();
                }
                else
                {
                    pharmacies = await _context.tbPharmacys.ToListAsync();
                }
            }
            else
            {
                pharmacies = await _context.tbPharmacys.ToListAsync();
            }

            List<Models.Entities.Pharmacy> filteredPharmacies = new List<Models.Entities.Pharmacy>();

            if (string.IsNullOrEmpty(searchString))
            {
                filteredPharmacies = pharmacies;
            }
            else
            {
                foreach (var user in pharmacies)
                {
                    var json = JsonConvert.SerializeObject(user);
                    var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    if (dictionary[filter] != null && dictionary[filter].ToUpper().Contains(searchString.ToUpper()))
                    {
                        filteredPharmacies.Add(user);
                    }
                }
            }

            return filteredPharmacies;
        }
        public async Task<List<Models.Entities.Pharmacy>> GetAvailablePharmacies(DateTime dateTime)
        {
            List<Models.Entities.Pharmacy> pharmacies = await _context.tbPharmacys.ToListAsync();

            List<Models.Entities.Pharmacy> filterdPharmacies = new List<Models.Entities.Pharmacy>();

            foreach (var pharma in pharmacies)
            {
                var entryPoint = await (from userrole in _context.UserRoles
                                        join role in _context.Roles on userrole.RoleId equals role.Id
                                        where role.Name == "Pharmacist"
                                        select userrole.UserId).ToListAsync();
                var users = await _context.AppUsers.Where(e => entryPoint.Contains(e.Id) && e.PharmacyId == pharma.Id).ToListAsync();

                foreach (var user in users)
                {
                    var appoitnments = await _context.tbAppointments.Where(x => x.MedicalExpertID == user.Id).ToListAsync();

                    var appoitnmentsFilterd = new List<Appointment>();

                    foreach (var app in appoitnments)
                    {
                        if (DateTime.Compare(app.StartDateTime, dateTime) < 0 && DateTime.Compare(app.StartDateTime + app.Duration, dateTime) > 0)
                        {
                            appoitnmentsFilterd.Add(app);
                            break;
                        }
                    }

                    if (appoitnmentsFilterd.Count == 0)
                    {
                        filterdPharmacies.Add(pharma);
                        break;
                    }
                }
            }
            return filterdPharmacies;
        }

        public async Task<List<AppUser>> GetAllPharmacists(long pharmacyId, DateTime dateTime)
        {
            var entryPoint = await (from userrole in _context.UserRoles
                                join role in _context.Roles on userrole.RoleId equals role.Id
                                where role.Name == "Pharmacist"
                                select userrole.UserId).ToListAsync();
            var users = await _context.AppUsers.Where(e => entryPoint.Contains(e.Id) && e.PharmacyId == pharmacyId).ToListAsync();


            var listUser = new List<AppUser>();
            foreach (var user in users)
            {
                var appoitnments = await _context.tbAppointments.Where(x => x.MedicalExpertID == user.Id).ToListAsync();

                var appoitnmentsFilterd = new List<Appointment>();

                foreach (var app in appoitnments)
                {
                    if (DateTime.Compare(app.StartDateTime, dateTime) < 0 && DateTime.Compare(app.StartDateTime + app.Duration, dateTime) > 0)
                    {
                        appoitnmentsFilterd.Add(app);
                        break;
                    }
                }

                if (appoitnmentsFilterd.Count == 0)
                {
                    listUser.Add(user);
                }
            }
            return listUser;
        }

        public async Task<List<Pharmacy.Models.Entities.Pharmacy>> GetAll()
        {
            return await _context.tbPharmacys.ToListAsync();
        }

        public async Task<List<long>> GetPharmacyByDermatologist(string id)
        {
            return await _context.DermatologistPharmacy.Where(x => x.UserId == id).Select(x => x.PharmacyId).ToListAsync();
        }
    }
}