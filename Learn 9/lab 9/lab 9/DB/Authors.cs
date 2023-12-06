using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_9.DB
{
    public class Authors
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BookAuthors> BookAuthors { get; set; }
    }
}
