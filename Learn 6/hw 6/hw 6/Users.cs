using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_6
{
    public class Users
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public bool IsAdmin { get; set; }
    }
}
