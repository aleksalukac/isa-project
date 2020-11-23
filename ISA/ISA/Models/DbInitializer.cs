using ISA.Models.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ISA.Models
{
    public static class DbInitializer
    {
        public static void Initialize(Context context)
        {
            //Deleting existing data base, before running existing one
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            
            // CHecking if rooms already exist 
            if (context.tbReports.Any())
            {
                return;   // If it's exist, skip adding new one
            }

            var students = new Report[]
            {
            new Report{Id= null, ReportText="Carson1"},
            new Report{Id= null, ReportText="Carson2"},
            new Report{Id= null, ReportText="Carson3"},
            new Report{Id= null, ReportText="Carson4"}
            };

            foreach (Report s in students)
            {
                context.tbReports.Add(s);
            }

            context.SaveChanges();

            
        }
    }
}
