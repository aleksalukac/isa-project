using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Entities
{
    public class TimeSpan
    {
        public TimeSpan(DateTime beginTime, DateTimeOffset duration)
        {
            BeginTime = beginTime;
            Duration = duration;
        }
        [Key]
        [Required]
        public long Id { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Hire Date")]
        public DateTime BeginTime { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Hire Date")]
        public DateTimeOffset Duration { get; set; }
    
        
    }
}
