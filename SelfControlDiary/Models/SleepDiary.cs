using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.Models
{
    public class SleepDiary : BaseEntity
    {
        public DateTime Lay { get; set; }
        public DateTime Up { get; set; }
        public TimeSpan HourSleep { get; set; }
        public int Grade { get; set; }
        public string UserId { get; set; }
    }
}
