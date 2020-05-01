using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM3.Models
{
    public class CustomerAccountProduct
    {
        public int ID { get; set; }
        public int CustomerAccoountId { get; set; }
        public int ProductId { get; set; }

        public CustomerAccount CustomerAccount { get; set; }
        public Product Product { get; set; }
    }
}
