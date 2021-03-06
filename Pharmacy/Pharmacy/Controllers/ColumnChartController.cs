﻿using Microsoft.AspNetCore.Mvc;
using Pharmacy.Data;
using Pharmacy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Pharmacy.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Models;

namespace Pharmacy.Controllers
{
    [Authorize(Roles ="PharmacyAdmin")]
    public class ColumnChartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ColumnChartController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: /<controller>/  
        public IActionResult Index()
        {
            return View();
        }

        public async Task<List<ChartModel>> GetCityPopulationListYearAsync()
        {
            var list = new List<ChartModel>();
            list.Add(new ChartModel { CityName = "This Year", PopulationYear2020 = (int)(await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction.Year == DateTime.Now.Year).ToListAsync()).Sum(item => item.Cost) });
            list.Add(new ChartModel { CityName = "Last Year", PopulationYear2020 = (int)(await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction.Year == DateTime.Now.Year - 1).ToListAsync()).Sum(item => item.Cost) });
            list.Add(new ChartModel { CityName = "2 Years Ago", PopulationYear2020 = (int)(await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction.Year == DateTime.Now.Year - 2).ToListAsync()).Sum(item => item.Cost) });
            list.Add(new ChartModel { CityName = "3 Years Ago", PopulationYear2020 = (int)(await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction.Year == DateTime.Now.Year - 3).ToListAsync()).Sum(item => item.Cost) });

            return list;
        }

        public async Task<List<ChartModel>> GetCityPopulationListQuarterAsync()
        {
            var list = new List<ChartModel>();
            list.Add(new ChartModel { CityName = "First Quarter", PopulationYear2020 = (int)(await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction <= DateTime.Now && x.TimeOfTransaction >= DateTime.Now.AddMonths(-3)).ToListAsync()).Sum(item => item.Cost) });
            list.Add(new ChartModel { CityName = "Second Quarter", PopulationYear2020 = (int)(await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction <= DateTime.Now.AddMonths(-3) && x.TimeOfTransaction >= DateTime.Now.AddMonths(-6)).ToListAsync()).Sum(item => item.Cost) });
            list.Add(new ChartModel { CityName = "Third Quarter", PopulationYear2020 = (int)(await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction <= DateTime.Now.AddMonths(-6) && x.TimeOfTransaction >= DateTime.Now.AddMonths(-9)).ToListAsync()).Sum(item => item.Cost) });
            list.Add(new ChartModel { CityName = "Forth Quarter", PopulationYear2020 = (int)(await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction <= DateTime.Now.AddMonths(-9) && x.TimeOfTransaction >= DateTime.Now.AddMonths(-12)).ToListAsync()).Sum(item => item.Cost) });

            return list;
        }

        public async Task<List<ChartModel>> GetCityPopulationListMonthAsync()
        {
            var list = new List<ChartModel>();
            
            for (int i = 0; i < 12; i++)
            {
                list.Add(new ChartModel { CityName = new DateTime(1, i+1, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("en-GB")), PopulationYear2020 = (int) (await _context.tbOrders.Where(x => x.TimeOfTransaction.Month == i+1 && (x.TimeOfTransaction.Year == DateTime.Now.Year || x.TimeOfTransaction.Year == DateTime.Now.Year - 1)).ToListAsync()).Sum(x => x.Cost) });

            }

            return list;
        }

        [HttpGet]
        public JsonResult PopulationChartYear()
        {
            var populationList = GetCityPopulationListYearAsync().Result;
            return Json(populationList);
        }
        
        [HttpGet]
        public JsonResult PopulationChartQuarter()
        {
            var populationList = GetCityPopulationListQuarterAsync().Result;
            return Json(populationList);
        }

        [HttpGet]
        public JsonResult PopulationChartMonth()
        {
            var populationList = GetCityPopulationListMonthAsync().Result;
            return Json(populationList);
        }


        public async Task<List<ChartModel>> GetAppoitmentListYearAsync()
        {
            var list = new List<ChartModel>();
            var user = await _userManager.GetUserAsync(User);
            list.Add(new ChartModel { CityName = "This Year", PopulationYear2020 = (int)(await _context.tbAppointments.Where(x => x.PhrmacyId == user.PharmacyId && x.StartDateTime.Year == DateTime.Now.Year).ToListAsync()).Count()});
            list.Add(new ChartModel { CityName = "Last Year", PopulationYear2020 = (int)(await _context.tbAppointments.Where(x => x.PhrmacyId == user.PharmacyId && x.StartDateTime.Year == DateTime.Now.Year - 1).ToListAsync()).Count() });
            list.Add(new ChartModel { CityName = "2 Years Ago", PopulationYear2020 = (int)(await _context.tbAppointments.Where(x => x.PhrmacyId == user.PharmacyId && x.StartDateTime.Year == DateTime.Now.Year - 2).ToListAsync()).Count() });
            list.Add(new ChartModel { CityName = "3 Years Ago", PopulationYear2020 = (int)(await _context.tbAppointments.Where(x => x.PhrmacyId == user.PharmacyId && x.StartDateTime.Year == DateTime.Now.Year - 3).ToListAsync()).Count() });

            return list;
        }

        public async Task<List<ChartModel>> GetAppoitmentQuarterAsync()
        {
            var list = new List<ChartModel>();
            var user = await _userManager.GetUserAsync(User);
            list.Add(new ChartModel { CityName = "First Quarter", PopulationYear2020 = (int)(await _context.tbAppointments.Where(x => x.PhrmacyId == user.PharmacyId && x.StartDateTime <= DateTime.Now && x.StartDateTime >= DateTime.Now.AddMonths(-3)).ToListAsync()).Count() });
            list.Add(new ChartModel { CityName = "Second Quarter", PopulationYear2020 = (int)(await _context.tbAppointments.Where(x => x.PhrmacyId == user.PharmacyId && x.StartDateTime <= DateTime.Now.AddMonths(-3) && x.StartDateTime >= DateTime.Now.AddMonths(-6)).ToListAsync()).Count() });
            list.Add(new ChartModel { CityName = "Third Quarter", PopulationYear2020 = (int)(await _context.tbAppointments.Where(x => x.PhrmacyId == user.PharmacyId && x.StartDateTime <= DateTime.Now.AddMonths(-6) && x.StartDateTime >= DateTime.Now.AddMonths(-9)).ToListAsync()).Count() });
            list.Add(new ChartModel { CityName = "Forth Quarter", PopulationYear2020 = (int)(await _context.tbAppointments.Where(x => x.PhrmacyId == user.PharmacyId && x.StartDateTime <= DateTime.Now.AddMonths(-9) && x.StartDateTime >= DateTime.Now.AddMonths(-12)).ToListAsync()).Count() });

            return list;
        }

        public async Task<List<ChartModel>> GetAppoitmentMonthAsync()
        {
            var list = new List<ChartModel>();
            var user = await _userManager.GetUserAsync(User);
            for (int i = 0; i < 12; i++)
            {
                list.Add(new ChartModel { CityName = new DateTime(1, i + 1, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("en-GB")), PopulationYear2020 = (int)(await _context.tbAppointments.Where(x => x.StartDateTime.Month == i + 1 && (x.StartDateTime.Year == DateTime.Now.Year || x.StartDateTime.Year == DateTime.Now.Year - 1)).ToListAsync()).Count() });

            }

            return list;
        }

        [HttpGet]
        public JsonResult AppoitmentChartYear()
        {
            var populationList = GetAppoitmentListYearAsync().Result;
            return Json(populationList);
        }

        [HttpGet]
        public JsonResult AppoitmentChartQuarter()
        {
            var populationList = GetAppoitmentQuarterAsync().Result;
            return Json(populationList);
        }

        [HttpGet]
        public JsonResult AppoitmentChartMonth()
        {
            var populationList = GetAppoitmentMonthAsync().Result;
            return Json(populationList);
        }


        public async Task<List<ChartModel>> GetTotalRevenueAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var list = new List<ChartModel>();
            for (int i = 0; i < 12; i++)
            {
                var priceApp = (int)(await _context.tbAppointments.Where(x => x.StartDateTime.Month == i + 1 && (x.StartDateTime.Year == DateTime.Now.Year || x.StartDateTime.Year == DateTime.Now.Year - 1)).ToListAsync()).Sum(app => app.Price);
                var priceDrugs = (int)(await _context.tbOrders.Where(x => x.TimeOfTransaction.Month == i + 1 && (x.TimeOfTransaction.Year == DateTime.Now.Year || x.TimeOfTransaction.Year == DateTime.Now.Year - 1)).ToListAsync()).Sum(x => x.Cost);
                list.Add(new ChartModel
                {
                    CityName = new DateTime(1, i + 1, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("en-GB")),
                    PopulationYear2020 = priceApp + priceDrugs
                });
            }

            return list;
        }

        [HttpGet]
        public JsonResult TotalRevenue()
        {
            var populationList = GetTotalRevenueAsync().Result;
            return Json(populationList);
        }
    }
}
