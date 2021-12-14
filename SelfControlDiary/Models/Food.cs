using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.Models
{
    public class Food : BaseEntity
    {
        public string FoodName { get; set; }
        public int Kalls { get; set; }
        public virtual List<FoodDiary> FoodDiary { get; set; }

        public string UserId { get; set; }
    }
}
