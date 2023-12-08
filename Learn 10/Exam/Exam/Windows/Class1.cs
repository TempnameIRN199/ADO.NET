using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Windows
{
    public class MedalTableRow
    {
        public string Country { get; set; }
        public string City { get; set; }
        public int GoldCount { get; set; }
        public int SilverCount { get; set; }
        public int BronzeCount { get; set; }
        public int TotalCount => GoldCount + SilverCount + BronzeCount;
    }
}
