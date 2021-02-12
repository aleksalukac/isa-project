using NUnit.Framework;
using Pharmacy.Controllers;
using Pharmacy.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Models.Entities.Users;
using Pharmacy.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace NUnitTestPharmacy.NUnitTests
{
    public class UnitTest6
    {

        public SupplyItemsController supplyItemsController;
        public DrugsController drugsController;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private ApplicationDbContext _context;
        public SupplyItem supply;

        [SetUp]
        public async Task SetUpAsync()
        {
            var dbOption = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Server=.\\SQLEXPRESS;data source=mssql11.orion.rs;initial catalog=isa;Password = UrosFic@Luk@c;Persist Security Info=True;User ID=aleksalukac;MultipleActiveResultSets=True;App=EntityFramework&quot;").Options;
            _context = new ApplicationDbContext(dbOption);
            supplyItemsController = new SupplyItemsController(_context);
            SupplyItem supplyOld = _context.SupplyItems.OrderByDescending(p => p.Id).FirstOrDefault();

            supply = new SupplyItem(supplyOld.Id+1, 1, 4, 2);
            var actionResult = supplyItemsController.Create(supply);
        }

        #region Unit Test
        [Test]
        public async Task InvalideDrugFormatValide()
        {
            supplyItemsController = new SupplyItemsController(_context);
            supplyItemsController.DeleteConfirmed(supply.Id + 1);
            var NullDrug = _context.SupplyItems.Find(supply.Id+1);
            Assert.IsNull(NullDrug);
        }
        #endregion


    }
}