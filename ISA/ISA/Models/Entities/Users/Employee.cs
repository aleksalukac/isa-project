using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ISA.Models.Entities.Users
{
    [Table("tbEmployees")]
    public class Employee : User
    {
        public Employee() 
        {

        }

        public Employee(User user,List<AbsenceRequest> absenceRequests, float salary, Roles role, TimeSpan workingSchedule, TimeSpan absenceTime)  : 
        base(user.Username, user.Email, user.Name, user.Surname, user.Appointments, user.Password)
        {
            AbsenceRequests = absenceRequests;
            Salary = salary;
            Role = role;
            AbsenceTime = absenceTime;
            WorkingSchedule = workingSchedule;
        }

        public TimeSpan WorkingSchedule { get; set; }
        public TimeSpan AbsenceTime { get; set; }
        public List<AbsenceRequest> AbsenceRequests { get; set; }


        //[ForeignKey("Pharmacy")]
        //public long PharmacyID { get; set; }

        //public Pharmacy Pharmacy { get; set; }
        public float Salary { get; private set; }
        public Roles Role { get; set; }

        public void SetSalary(float salary)
        {
            if (salary < 0)
                throw new Exception("Salary cannot be negative");

            Salary = salary;
        }
    }
}
