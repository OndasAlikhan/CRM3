using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM3.Models
{
    public class CustomerAccount
    {
        public int ID { get; set; }
        public int CustomerId { get; set; }
        public int FilialId { get; set; }


        public Customer Customer { get; set; }
        public Filial Filial { get; set; }
        public ICollection<CustomerAccountProduct> CustomerAccountProducts { get; set; } 
    }
}
