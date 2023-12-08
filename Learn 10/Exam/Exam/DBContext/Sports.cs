using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DBContext
{
    public class Sports
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AmountMember { get; set; }

        public int OlympicId { get; set; }
        public virtual Olympics Olympics { get; set; }

        public virtual ICollection<Members> Members { get; set; }
        public virtual ICollection<Results> Results { get; set; }
    }
}
