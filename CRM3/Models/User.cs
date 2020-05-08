using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM3.Models
{
    // User represents the operator or admin that work in this CRM
    public class User : IdentityUser
    {
        public override string Email { get; set; }
        public string Password { get; set; }

        //public ICollection<UserRole> UserRoles { get; set; }
    }
}
