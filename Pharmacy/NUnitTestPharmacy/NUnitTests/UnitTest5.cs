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
    public class UnitTest5
    {

        public SupplyItemsController supplyItemsController;
        public DrugsController drugsController;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private ApplicationDbContext _context;

        [SetUp]
        public async Task SetUpAsync()
        {
            var dbOption = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Server=.\\SQLEXPRESS;data source=mssql11.orion.rs;initial catalog=isa;Password = UrosFic@Luk@c;Persist Security Info=True;User ID=aleksalukac;MultipleActiveResultSets=True;App=EntityFramework&quot;").Options;
            _context = new ApplicationDbContext(dbOption);
            supplyItemsController = new SupplyItemsController( _context);
        }

        public SupplyItem ValidateDrugEnter(long drugId)
        {
            return _context.SupplyItems.Find(drugId);
        }

        #region Unit Test
        [Test]
        public async Task InvalideDrugFormatValide()
        {
            SupplyItem supply = await _context.SupplyItems.FirstOrDefaultAsync();
            long supplyBeforeEdit = supply.ExtraQuantity;
            supply.ExtraQuantity = supply.ExtraQuantity + 1;
            var actionResult = await supplyItemsController.Edit(supply.Id, supply);
            var drugInDb = ValidateDrugEnter(supply.Id);
            Assert.AreNotEqual(supplyBeforeEdit, drugInDb.ExtraQuantity);
        }
        #endregion

        
    }
}