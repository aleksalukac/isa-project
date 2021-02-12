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
    public class UnitTest4
    {

        public SupplyItemsController supplyItemsController;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;


        #region Unit Test
        [Test]
        public async Task InvalideSupplyItemValide()
        {
            SupplyItem supply = new SupplyItem(1, 1, 1, -1);
            supplyItemsController = new SupplyItemsController(_context);
            var actionResult = await supplyItemsController.Create(supply);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult, typeof(BadRequestObjectResult).ToString());
        }
        #endregion

        
    }
}