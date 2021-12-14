using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.Models
{
    public class PhysicalDiary : BaseEntity
    {
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int PhysicalsId { get; set; }
        public virtual Physical Physicals { get; set; }
        public virtual People Peoples { get; set; }
        public double weight { get; set; }
        public int Min { get; set; }
        public int Kall { get; set; }
        public string UserId { get; set; }
    }
}
