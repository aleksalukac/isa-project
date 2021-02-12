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
    public class UnitTest13
    {

        public OrderService _orderService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IDrugAndQuantitiesService _drugAndQuantitiesService;
        private ApplicationDbContext _context;

        [SetUp]
        public async Task SetUpAsync()
        {
            var dbOption = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Server=.\\SQLEXPRESS;data source=mssql11.orion.rs;initial catalog=isa;Password = UrosFic@Luk@c;Persist Security Info=True;User ID=aleksalukac;MultipleActiveResultSets=True;App=EntityFramework&quot;").Options;
            _context = new ApplicationDbContext(dbOption);
            //drugAndQuantitiesController = new DrugAndQuantitiesController(_context, _userManager, new DrugAndQuantitiesService(_context));
            _orderService = new OrderService(_context);

        }

        #region Unit Test
        [Test]
        public async Task IsOverlappingValideAsync()
        {
            var adminExit = await _orderService.IsOrderCompleted(-1);
            Assert.IsFalse(adminExit);
        }
        #endregion
    }
}