using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using Pharmacy;
using Pharmacy.Controllers;
using Pharmacy.Models.DTO;
using Pharmacy.Models.Entities;
using Pharmacy.Areas.Identity.Pages.Account;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Models.Entities.Users;
using System.Web.Http.Results;
using Pharmacy.Data;
using System.Threading.Tasks;

namespace NUnitTestPharmacy
{
    public class Tests
    {

        public SupplyOrdersController supplyOrdersController;
        public DrugsController drugsController;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ApplicationDbContext _context;

        [Test]
        public async Task InvalideDrugFormatValide()
        {
            Drug drug = new Drug(-1, "Neso", DrugForm.BuccalFilm, "Nes", "Nes", true, "Nes");
            drugsController = new DrugsController(_context, _userManager);
            var actionResult = await drugsController.Create(drug);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult, typeof(BadRequestObjectResult).ToString());
        }
    }
}