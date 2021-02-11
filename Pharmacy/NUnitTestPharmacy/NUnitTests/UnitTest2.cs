using NUnit.Framework;
using Pharmacy.Controllers;
using Pharmacy.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Models.Entities.Users;
using Pharmacy.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NUnitTestPharmacy.NUnitTests
{
    public class UnitTest2
    {

        public SupplyOrdersController supplyOrdersController;
        public DrugsController drugsController;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private ApplicationDbContext _context;

        [SetUp]
        public async Task SetUpAsync()
        {
            var dbOption = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Server=.\\SQLEXPRESS;data source=mssql11.orion.rs;initial catalog=isa;Password = UrosFic@Luk@c;Persist Security Info=True;User ID=aleksalukac;MultipleActiveResultSets=True;App=EntityFramework&quot;").Options;
            _context = new ApplicationDbContext(dbOption);
            drugsController = new DrugsController(_context, _userManager);
        }

        public Drug ValidateDrugEnter(long drugId)
        {
            return _context.tbDrugs.Find(drugId);
        }

        #region Unit Test
        [Test]
        public async Task InvalideDrugFormatValide()
        {
            Drug drug = new Drug("Neso", DrugForm.BuccalFilm, "Nes", "Nes", true, "Nes");
            var actionResult = await drugsController.Edit(drug.Id, drug);
            var drugInDb = ValidateDrugEnter(drug.Id);
            Assert.AreEqual(drug, drugInDb);
        }
        #endregion

        
    }
}