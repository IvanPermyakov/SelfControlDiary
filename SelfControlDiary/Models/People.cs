using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.Models
{
    public class People : BaseEntity
    {
        public int SexId { get; set; }
        public virtual Sex Sexs { get; set; }
        public int Age { get; set; }
        public double weight { get; set; }
        public double height { get; set; }
        public int ActivId { get; set; }
        public virtual Activ Activs { get; set; }
        public double BMR
        {
            get
            {
                if (SexId == 1)
                    return 88.36 + (13.4 * weight) + (4.8 * height) - (5.7 * Age); 
                else
                    return 447.6 + (9.2 * weight) + (3.1 * height) - (4.3 * Age);
            }
        }
        public int Norma
        {
            get
            {
                return Convert.ToInt32(BMR * Activs.Value);
            }
        }
        public string UserId { get; set; }

    }
}
