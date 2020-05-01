using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM3.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        [Remote(action: "VerifyName", controller: "Customer")]
        public string FullName { get; set; }
        [Required]
        [MaxLength(14)]
        public string Phone { get; set; }

        public ICollection<CustomerAccount> CustomerAccounts { get; set; }
    }
}
