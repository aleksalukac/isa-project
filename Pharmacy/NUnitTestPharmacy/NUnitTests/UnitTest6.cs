using NUnit.Framework;
using Pharmacy.Controllers;
using Pharmacy.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Models.Entities.Users;
using Pharmacy.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Services;

namespace NUnitTestPharmacy.NUnitTests
{
    public class UnitTest6
    {

        public AbsenceRequestsController absencesController;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private  ApplicationDbContext _context;
        public AbsenceRequest drug;
        /*
        [SetUp]
        public async Task SetUpAsync()
        {
            var dbOption = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Server=.\\SQLEXPRESS;data source=mssql11.orion.rs;initial catalog=isa;Password = UrosFic@Luk@c;Persist Security Info=True;User ID=aleksalukac;MultipleActiveResultSets=True;App=EntityFramework&quot;").Options;
            _context = new ApplicationDbContext(dbOption);
            absencesController = new AbsenceRequestsController(_context, _userManager, _signInManager, new UserService(_userManager) ,new AbsenceRequestService(_context), IEma, new PharmacyService());
            /*
              IAbsenceRequestService absenceRequestService,
                IEmailSender emailSender, IPharmacyService pharmacyService/*
            drug = new AbsenceRequest("Neso", DrugForm.BuccalFilm, "Nes", "Nes", true, "Nes");
            drug.Id = await _context.tbDrugs.CountAsync() + 1;
            var actionResult = drugsController.Create(drug);
        }

        #region Unit Test
        [Test]
        public async Task InvalideDrugFormatValide()
        {   
            drugsController = new DrugsController(_context, _userManager);
            drugsController.DeleteConfirmed(drug.Id);
            var NullDrug = _context.tbDrugs.Find(drug.Id);
            Assert.IsNull(NullDrug);
        }
        #endregion
    */


    }
}