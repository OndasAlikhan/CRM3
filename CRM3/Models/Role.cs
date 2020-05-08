using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM3.Models
{
    public class Role : IdentityRole
    {
        [Required]
        [MaxLength(100)]
        public override string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
