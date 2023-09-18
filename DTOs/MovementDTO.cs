using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardApi.DTOs
{
    public class MovementDTO
    {
        public float Amount {get; set;}
        public string Description {get; set;}
        public string BusinessName {get; set;}
        public string currency {get; set;}

        public MovementDTO(string description, string businessName, string currency, float amount)
        {
            this.Description = description;
            this.BusinessName = businessName;
            this.currency = currency;
            this.Amount = amount;
        }
    }
}