using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DBContext
{
    public class Members
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public DateTime DOB { get; set; }

        public int SportId { get; set; }
        public virtual Sports Sports { get; set; }

        public virtual ICollection<Results> Results { get; set; }
    }
}
