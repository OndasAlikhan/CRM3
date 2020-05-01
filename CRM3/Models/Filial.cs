using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM3.Models;

namespace CRM3.Models
{
    public class Filial
    {
        public int ID { get; set; }

        [IsKazakhstan]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
