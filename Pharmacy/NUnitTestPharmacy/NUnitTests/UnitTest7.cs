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
    public class UnitTest7
    {

        public AbsenceRequestsController absenceRequestsController;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private ApplicationDbContext _context;
        public AbsenceRequest absence;
        public long toDeleteId;

        [SetUp]
        public async Task SetUpAsync()
        {
            var dbOption = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Server=.\\SQLEXPRESS;data source=mssql11.orion.rs;initial catalog=isa;Password = UrosFic@Luk@c;Persist Security Info=True;User ID=aleksalukac;MultipleActiveResultSets=True;App=EntityFramework&quot;").Options;
            _context = new ApplicationDbContext(dbOption);
            absenceRequestsController = new AbsenceRequestsController(_context, _userManager, _signInManager, null, new AbsenceRequestService(_context), null, new PharmacyService(_context));

            absence = new AbsenceRequest();

            _ = await _context.AddAsync(absence);
            await _context.SaveChangesAsync();

            toDeleteId = _context.tbAbsenceRequests.OrderByDescending(p => p.Id).FirstOrDefault().Id;

        }

        #region Unit Test
        [Test]
        public async Task InvalideDrugFormatValide()
        {
            await absenceRequestsController.DeleteConfirmed(toDeleteId);
            var NullDrug = _context.tbAbsenceRequests.Find(toDeleteId);
            Assert.IsNull(NullDrug);
        }
        #endregion
    }
}