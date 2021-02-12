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
    public class UnitTest9
    {

        public DrugAndQuantitiesController drugAndQuantitiesController;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IDrugAndQuantitiesService _drugAndQuantitiesService;
        private ApplicationDbContext _context;
        public DrugAndQuantities absence;
        public long toDeleteId;

        [SetUp]
        public async Task SetUpAsync()
        {
            var dbOption = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Server=.\\SQLEXPRESS;data source=mssql11.orion.rs;initial catalog=isa;Password = UrosFic@Luk@c;Persist Security Info=True;User ID=aleksalukac;MultipleActiveResultSets=True;App=EntityFramework&quot;").Options;
            _context = new ApplicationDbContext(dbOption);
            drugAndQuantitiesController = new DrugAndQuantitiesController(_context, _userManager, new DrugAndQuantitiesService(_context));
            
            absence = new DrugAndQuantities();

            _ = await _context.AddAsync(absence);
            await _context.SaveChangesAsync();

            toDeleteId = _context.DrugAndQuantity.OrderByDescending(p => p.Id).FirstOrDefault().Id;
        }

        #region Unit Test
        [Test]
        public async Task InvalideDrugFormatValide()
        {
            await drugAndQuantitiesController.DeleteConfirmed(toDeleteId);
            var NullDrug = _context.DrugAndQuantity.Find(toDeleteId);
            Assert.IsNull(NullDrug);
        }
        #endregion
    }
}