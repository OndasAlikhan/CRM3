using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM3.Models
{
    public class IsKazakhstanAttribute : ValidationAttribute
    {
        private List<string> KazFilials;
        public IsKazakhstanAttribute()
        {
             KazFilials = new List<String>(){ "Almaty", "Astana", "Aktau", "Shymkent", "Semey", "Oskement", "Pavlodar", "Petropavlovks", "Atyrau", "Aktobe", "Oral", "Kostanay", "Kyzylorda", "Taraz", "Karaganda"};
        }

        public string GetErrorMessage() => "That filial is not existent in Kazakhstan";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (KazFilials.Contains(value))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(GetErrorMessage());
            }
        }

    }
}
