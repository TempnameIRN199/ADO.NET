using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_9.DB
{
    public class BookAuthors
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        public virtual Books Book { get; set; }
        public virtual Authors Author { get; set; }
    }
}
