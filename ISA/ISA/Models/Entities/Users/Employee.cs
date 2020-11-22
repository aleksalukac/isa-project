using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISA.Models.Entities.Users
{
    public class Employee : User
    {
        public TimeSpan WorkingSchedule { get; set; }
        public TimeSpan AbsenceTime { get; set; }
        public AbsenceRequest AbsenceRequest { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public float Salary { get; private set; }

        public void SetSalary(float salary)
        {
            if (salary < 0)
                throw new Exception("Salary cannot be negative");

            Salary = salary;
        }
    }
}
