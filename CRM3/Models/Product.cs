using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM3.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<CustomerAccountProduct> CustomerAccountProducts { get; set; }
    }
}
