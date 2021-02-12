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
        public long toDeleteId;

        [SetUp]
        public async Task SetUpAsync()
        {
            var dbOption = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Server=.\\SQLEXPRESS;data source=mssql11.orion.rs;initial catalog=isa;Password = UrosFic@Luk@c;Persist Security Info=True;User ID=aleksalukac;MultipleActiveResultSets=True;App=EntityFramework&quot;").Options;
            _context = new ApplicationDbContext(dbOption);
            supplyItemsController = new SupplyItemsController(_context);
            SupplyItem supplyOld = _context.SupplyItems.OrderByDescending(p => p.Id).FirstOrDefault();
            toDeleteId = supplyOld.Id + 1;
            supply = new SupplyItem();

            _ = await _context.AddAsync(supply);
            await _context.SaveChangesAsync();
        }

        #region Unit Test
        [Test]
        public async Task InvalideDrugFormatValide()
        {
            supplyItemsController = new SupplyItemsController(_context);
            await supplyItemsController.DeleteConfirmed(toDeleteId);
            var NullDrug = _context.SupplyItems.Find(toDeleteId);
            Assert.IsNull(NullDrug);
        }
        #endregion


    }
}