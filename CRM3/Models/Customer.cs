using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM3.Models
{
    public class Customer
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [Phone]
        public string Phone { get; set; }

        public ICollection<CustomerAccount> CustomerAccounts { get; set; }
    }
}
