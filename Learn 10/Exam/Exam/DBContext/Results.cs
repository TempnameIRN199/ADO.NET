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
        public int MembersId { get; set; }
        public int SportsId { get; set; }

        public virtual Members Members { get; set; }
        public virtual Sports Sports { get; set; }
    }
}
