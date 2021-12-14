using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.Models
{
    public class Activ : BaseEntity
    {
        public float Value { get; set; }
        public virtual List<People> People { get; set; }
    }
}
