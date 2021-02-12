using Pharmacy.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Models.Entities.Users;
using Pharmacy.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using NUnit.Framework;
using Pharmacy.Controllers;
using Pharmacy.Services;

namespace NUnitTestPharmacy.NUnitTests
{
    public class UnitTest10
    {

        public AppointmentService appointmentService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IDrugAndQuantitiesService _drugAndQuantitiesService;
        private ApplicationDbContext _context;
        public DrugAndQuantities absence;
        public long toDeleteId;
        public Appointment appoitment1;
        public Appointment appoitment2;

    [SetUp]
        public async Task SetUpAsync()
        {
            var dbOption = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Server=.\\SQLEXPRESS;data source=mssql11.orion.rs;initial catalog=isa;Password = UrosFic@Luk@c;Persist Security Info=True;User ID=aleksalukac;MultipleActiveResultSets=True;App=EntityFramework&quot;").Options;
            _context = new ApplicationDbContext(dbOption);
            //drugAndQuantitiesController = new DrugAndQuantitiesController(_context, _userManager, new DrugAndQuantitiesService(_context));
            appointmentService = new AppointmentService(_context, new AbsenceRequestService(_context));

            appoitment1 = new Appointment();
            appoitment2 = new Appointment();

            appoitment1.StartDateTime = new System.DateTime(1, 1, 1, 1, 1, 1);
            appoitment1.Duration = new System.TimeSpan(0, 11, 0);

            appoitment2.StartDateTime = new System.DateTime(1, 1, 1, 1, 10, 1);
            appoitment2.Duration = new System.TimeSpan(0, 40, 0);
        }

        #region Unit Test
        [Test]
        public async Task InvalideDrugFormatValide()
        {
            Assert.IsFalse(appointmentService.IsOverlapping(
                appoitment1.StartDateTime,
                appoitment1.StartDateTime + appoitment1.Duration,
                appoitment2.StartDateTime,
                appoitment2.StartDateTime + appoitment2.Duration));
        }
        #endregion
    }
}