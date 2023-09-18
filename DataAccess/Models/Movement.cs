using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CreditCardApi.DTOs.Interfaces;

namespace CreditCardApi.DataAccess.Models



{
    public class Movement : IMovement
    {
        [Key]
        public float Amount {get; set;}
        public DateTime Date {get; set;}
        public string Description {get; set;}
        public string BusinessName {get; set;}
        public string currency {get; set;}
        public long cardId {get; set;}
        public long movementId {get; set;}

        public Movement(float amount, DateTime date, string description, string businessName, string currency, long cardId, long movementId)
        {
            this.Amount = amount;
            this.Date = date;
            this.Description = description;
            this.BusinessName = businessName;
            this.currency = currency;
            this.cardId = cardId;
            this.movementId = movementId;
        }
    }
}