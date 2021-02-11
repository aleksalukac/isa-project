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
    public class UnitTest3
    {

        public SupplyOrdersController supplyOrdersController;
        public DrugsController drugsController;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private  ApplicationDbContext _context;
        public Drug drug;

        [SetUp]
        public async Task SetUpAsync()
        {
            var dbOption = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Server=.\\SQLEXPRESS;data source=mssql11.orion.rs;initial catalog=isa;Password = UrosFic@Luk@c;Persist Security Info=True;User ID=aleksalukac;MultipleActiveResultSets=True;App=EntityFramework&quot;").Options;
            _context = new ApplicationDbContext(dbOption);
            drugsController = new DrugsController(_context, _userManager);
            drug = new Drug("Neso", DrugForm.BuccalFilm, "Nes", "Nes", true, "Nes");
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


    }
}