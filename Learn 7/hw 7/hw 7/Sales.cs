using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_7
{
    public class Sales
    {

        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int ProductId { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
