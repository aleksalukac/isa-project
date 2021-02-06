using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public bool ShowPharmacyToolBar { get; set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "PharmacyAdmin")]
        public IActionResult Pharmacy()
        {
            return Redirect("/Pharmacies");
        }

        [Authorize(Roles = "PharmacyAdmin")]
        public IActionResult Appointments()
        {
            return Redirect("/Appointments");
        }

        [Authorize(Roles = "PharmacyAdmin")]
        public IActionResult AbsenceRequests()
        {
            return Redirect("/AbsenceRequests");
        }
        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
