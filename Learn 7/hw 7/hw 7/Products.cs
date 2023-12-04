using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace hw_7
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
