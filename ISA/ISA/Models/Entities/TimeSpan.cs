using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISA.Models.Entities
{
    public class TimeSpan : BaseEntity
    {
        public TimeSpan(DateTime beginTime, DateTime endTime)
        {
            BeginTime = beginTime;
            EndTime = endTime;
        }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Hire Date")]
        public DateTime BeginTime { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Hire Date")]
        public DateTime EndTime { get; set; }
    
        
    }
}
