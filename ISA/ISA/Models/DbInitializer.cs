using ISA.Models.Entities;
using ISA.Models.Entities.Users;
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
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            

            // CHecking if rooms already exist 
            if (context.tbReports.Any())
            {
                return;   // If it's exist, skip adding new one
            }
            else 
            {
                Entities.Users.User user = new User("urke", "urke@gmail.com", "Uros", "Urosevic", null, "87654321");
                context.tbUsers.Add(user);
                context.tbEmployees.Add(new Employee(user, null, (float)0.0, Roles.SystemAdministrator, null, new Entities.TimeSpan(DateTime.Now, DateTime.Today)));

                var students = new Report[]
                {
                new Report{User = user, ReportText="Nece"},
                new Report{User = user, ReportText="Uzas"},
                new Report{User = user, ReportText="Gad"},
                new Report{User = user, ReportText="Ozaj softver"}
                };

                foreach (Report s in students)
                {
                    context.tbReports.Add(s);
                }
            }

            

            context.SaveChanges();

            
        }
    }
}
