using Microsoft.AspNetCore.Mvc;
using Pharmacy.Data;
using Pharmacy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Pharmacy.Controllers
{
    public class ColumnChartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ColumnChartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/  
        public IActionResult Index()
        {
            return View();
        }

        public class PopulationModelYear
        {
            public string Name {get; set;}
            public double Value { get; set; }/*
            public double thisYear { get; set; }
            public double lastYear { get; set; }
            public double twoYearsAgo { get; set; }
            public double threeYearsAgo { get; set; }
            public double fourYearsAgo { get; set; }*/
        }

        

        public class PopulationModel
        {
            public string CityName { get; set; }
            public int PopulationYear2020 { get; set; }
        }

        public async Task<List<PopulationModel>> GetCityPopulationListYearAsync()
        {
            /*
            var list = new List<PopulationModelYear>();
            PopulationModelYear model = new PopulationModelYear();

            model.Name = "This Year";
            model.Value = (await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction.Year == DateTime.Now.Year).ToListAsync()).Sum(item => item.Cost);
            list.Add(model);

            PopulationModelYear model2 = new PopulationModelYear();

            model2.Name = "Last Year";
            model2.Value = (await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction.Year == DateTime.Now.Year - 1).ToListAsync()).Sum(item => item.Cost);
            list.Add(model2);*/


            var list = new List<PopulationModel>();
            list.Add(new PopulationModel { CityName = "This Year", PopulationYear2020 = (int)(await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction.Year == DateTime.Now.Year).ToListAsync()).Sum(item => item.Cost) });
            list.Add(new PopulationModel { CityName = "Last Year", PopulationYear2020 = (int)(await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction.Year == DateTime.Now.Year - 1).ToListAsync()).Sum(item => item.Cost) });
            list.Add(new PopulationModel { CityName = "2 Years Ago", PopulationYear2020 = (int)(await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction.Year == DateTime.Now.Year - 2).ToListAsync()).Sum(item => item.Cost) });
            list.Add(new PopulationModel { CityName = "3 Years Ago", PopulationYear2020 = (int)(await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction.Year == DateTime.Now.Year - 3).ToListAsync()).Sum(item => item.Cost) });

            
            return list;
        }

        public async Task<List<PopulationModel>> GetCityPopulationListQuarterAsync()
        {
            var list = new List<PopulationModel>();
            list.Add(new PopulationModel { CityName = "First Quarter", PopulationYear2020 = (int)(await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction <= DateTime.Now && x.TimeOfTransaction >= DateTime.Now.AddMonths(-3)).ToListAsync()).Sum(item => item.Cost) });
            list.Add(new PopulationModel { CityName = "Second Quarter", PopulationYear2020 = (int)(await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction <= DateTime.Now.AddMonths(-3) && x.TimeOfTransaction >= DateTime.Now.AddMonths(-6)).ToListAsync()).Sum(item => item.Cost) });
            list.Add(new PopulationModel { CityName = "Third Quarter", PopulationYear2020 = (int)(await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction <= DateTime.Now.AddMonths(-6) && x.TimeOfTransaction >= DateTime.Now.AddMonths(-9)).ToListAsync()).Sum(item => item.Cost) });
            list.Add(new PopulationModel { CityName = "Forth Quarter", PopulationYear2020 = (int)(await _context.tbOrders.Where(x => x.TransactionComplete == true && x.TimeOfTransaction <= DateTime.Now.AddMonths(-9) && x.TimeOfTransaction >= DateTime.Now.AddMonths(-12)).ToListAsync()).Sum(item => item.Cost) });


            return list;
        }

        public async Task<List<PopulationModel>> GetCityPopulationListMonthAsync()
        {
            var list = new List<PopulationModel>();
            //var months = (await _context.tbOrders.ToListAsync());


            for (int i = 0; i < 12; i++)
            {
                list.Add(new PopulationModel { CityName = new DateTime(1, i+1, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("en-GB")), PopulationYear2020 = (int) (await _context.tbOrders.Where(x => x.TimeOfTransaction.Month == i+1 && (x.TimeOfTransaction.Year == DateTime.Now.Year || x.TimeOfTransaction.Year == DateTime.Now.Year - 1)).ToListAsync()).Sum(x => x.Cost) });

            }
            /*for (int i = 0; i < trendData.Count; i++)
            {
                list.Add(new PopulationModel { CityName = new DateTime(1, i, 1).Month.ToString(), PopulationYear2020 = (int)trendData[i].Sum(x => x.Cost) });
            }*/

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
    }
}
