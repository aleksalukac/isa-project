using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.DTO
{
    public class AppointmentTimeDTO
    {
        public AppointmentTimeDTO(DateTime start, TimeSpan duration)
        {
            Start = start;
            End = start + duration;
        }

        public AppointmentTimeDTO(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
