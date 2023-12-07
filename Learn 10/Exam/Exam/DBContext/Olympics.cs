using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DBContext
{
    public class Olympics
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public EnumSeason Season { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public virtual ICollection<Sports> Sports { get; set; }
    }
}
