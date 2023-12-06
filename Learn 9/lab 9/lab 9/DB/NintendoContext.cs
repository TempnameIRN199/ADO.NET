using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_9.DB
{
    public class NintendoContext : DbContext
    {
        public NintendoContext() : base("Nintendo")
        {

        }

        public DbSet<Clients> Clients { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Rents> Rents { get; set; }
        public DbSet<BookAuthors> BookAuthors { get; set; }
    }
}
