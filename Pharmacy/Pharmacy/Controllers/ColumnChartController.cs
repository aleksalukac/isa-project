using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Controllers
{
    public class ColumnChartController : Controller
    {
        // GET: /<controller>/  
        public IActionResult Index()
        {
            return View();
        }

        public class PopulationModel
        {
            public string CityName { get; set; }
            public int PopulationYear2020 { get; set; }
            public int PopulationYear2010 { get; set; }
            public int PopulationYear2000 { get; set; }
            public int PopulationYear1990 { get; set; }

        }

        public static List<PopulationModel> GetCityPopulationList()
        {
            var list = new List<PopulationModel>();
            list.Add(new PopulationModel { CityName = "PURI", PopulationYear2020 = 28000, PopulationYear2010 = 45000, PopulationYear2000 = 22000, PopulationYear1990 = 50000 });
            list.Add(new PopulationModel { CityName = "Bhubaneswar", PopulationYear2020 = 30000, PopulationYear2010 = 49000, PopulationYear2000 = 24000, PopulationYear1990 = 39000 });
            list.Add(new PopulationModel { CityName = "Cuttack", PopulationYear2020 = 35000, PopulationYear2010 = 56000, PopulationYear2000 = 26000, PopulationYear1990 = 41000 });
            list.Add(new PopulationModel { CityName = "Berhampur", PopulationYear2020 = 37000, PopulationYear2010 = 44000, PopulationYear2000 = 28000, PopulationYear1990 = 48000 });
            list.Add(new PopulationModel { CityName = "Odisha", PopulationYear2020 = 40000, PopulationYear2010 = 38000, PopulationYear2000 = 30000, PopulationYear1990 = 54000 });

            return list;

        }

        [HttpGet]
        public JsonResult PopulationChart()
        {
            var populationList = GetCityPopulationList();
            return Json(populationList);
        }
    }
}
