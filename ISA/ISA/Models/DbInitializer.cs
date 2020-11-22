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
            context.Database.EnsureCreated();

            // Look for any students.
            
            if (context.tbReports.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Report[]
            {
            new Report{Id= null, ReportText="Carson"}
            };
            foreach (Report s in students)
            {
                context.tbReports.Add(s);
            }
            context.SaveChanges();

            
        }
    }
}
