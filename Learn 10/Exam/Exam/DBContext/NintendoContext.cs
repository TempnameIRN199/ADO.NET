using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DBContext
{
    public class NintendoContext : DbContext
    {
        public NintendoContext() : base("Nintendo")
        {

        }

        public DbSet<Olympics> Olympics { get; set; }
        public DbSet<Sports> Sports { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<Results> Results { get; set; }
    }
}
