using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.Models
{
    public class Physical : BaseEntity
    {
        public string Exercise { get; set; }
        public float Kall { get; set; }
        public virtual List<PhysicalDiary> PhysicalDiary { get; set; }
    }
}
