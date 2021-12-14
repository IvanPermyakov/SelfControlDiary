using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.Models
{
    public class FoodDiary : BaseEntity
    {
        public DateTime Date { get; set; }
        public int FoodsId { get; set; }
        public virtual Food Foods { get; set; }
        public int Grams { get; set; }
        public int Kalls { get; set; }
        /*{
            get
            {
                return Foods.Kalls * Grams / 100;
            }
        }*/
        public string UserId { get; set; }
    }
}
