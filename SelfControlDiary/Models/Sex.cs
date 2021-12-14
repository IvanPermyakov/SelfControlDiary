using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.Models
{
    public class Sex : BaseEntity
    {
        public string Value { get; set; }
        public virtual List<People> Peoples { get; set; }
    }
}
