using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CreditCardApi.DTOs.Interfaces;
using CreditCardApi.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace CreditCardApi.Models
{
    public class CreditCard : ICard
    {
        [Key]
        public long CardId { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerId { get; set; }
        public CardType CardType { get; set; }

        public float Limit { get; set; }

        public float AmountSpent { get; set; }

        public CreditCard(long cardId, string ownerFirstName, string ownerLastName, string ownerId, CardType cardType, float limit, float amountSpent)
        {
            this.CardId = cardId;
            this.OwnerFirstName = ownerFirstName;
            this.OwnerLastName = ownerLastName;
            this.OwnerId = ownerId;
            this.CardType = cardType;
            this.Limit = limit;
            this.AmountSpent = amountSpent;
        }

        public bool changeAmount(int amount)
        {
            if ((amount <= 0) || (this.AmountSpent + amount >= this.Limit))
            {
                return false;
            }
            this.AmountSpent += amount;
            return true;
        }
        
        public void printCard(){
            
        }
    }
}