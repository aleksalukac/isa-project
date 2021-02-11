using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Pharmacy.Controllers;
using Pharmacy.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Models.Entities.Users;
using Pharmacy.Data;
using System.Threading.Tasks;

namespace NUnitTestPharmacy.NUnitTests
{
    public class UnitTest1
    {

        public SupplyOrdersController supplyOrdersController;
        public DrugsController drugsController;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;


        #region Unit Test
        [Test]
        public async Task InvalideDrugFormatValide()
        {
            Drug drug = new Drug(-1, "Neso", DrugForm.BuccalFilm, "Nes", "Nes", true, "Nes");
            drugsController = new DrugsController(_context, _userManager);
            var actionResult = await drugsController.Create(drug);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult, typeof(BadRequestObjectResult).ToString());
        }
        #endregion

        
    }
}