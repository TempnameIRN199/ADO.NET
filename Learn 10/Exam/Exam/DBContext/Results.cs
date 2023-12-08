using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DBContext
{
    public class Results
    {
        public int Id { get; set; }
        public EnuMedal Medal { get; set; }
        
        public int MemberId { get; set; }
        public virtual Members Members { get; set; }

        public int SportId { get; set; }
        public virtual Sports Sports { get; set; }

    }
}
