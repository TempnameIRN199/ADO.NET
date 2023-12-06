using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_9.DB
{
    public class Rents
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int BookId { get; set; }

        public virtual Clients Client { get; set; }
        public virtual Books Book { get; set; }
    }
}
