using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_9.DB
{
    public class Clients
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool IsDebtor { get; set; }

        public virtual ICollection<Rents> Rents { get; set; }
    }
}
