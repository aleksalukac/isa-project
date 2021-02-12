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
    public class UnitTest8
    {

        public OrdersController ordersController;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private ApplicationDbContext _context;
        public Order absence;
        public long toDeleteId;

        [SetUp]
        public async Task SetUpAsync()
        {
            var dbOption = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Server=.\\SQLEXPRESS;data source=mssql11.orion.rs;initial catalog=isa;Password = UrosFic@Luk@c;Persist Security Info=True;User ID=aleksalukac;MultipleActiveResultSets=True;App=EntityFramework&quot;").Options;
            _context = new ApplicationDbContext(dbOption);
            ordersController = new OrdersController(_userManager, _context, new OrderService(_context), null, null);
            absence = new Order();

            _ = await _context.AddAsync(absence);
            await _context.SaveChangesAsync();

            toDeleteId = _context.tbOrders.OrderByDescending(p => p.Id).FirstOrDefault().Id;
        }

        #region Unit Test
        [Test]
        public async Task InvalideDrugFormatValide()
        {
            await ordersController.DeleteConfirmed(toDeleteId);
            var NullDrug = await _context.tbOrders.FindAsync(toDeleteId);
            Assert.IsNull(NullDrug);
        }
        #endregion
    }
}