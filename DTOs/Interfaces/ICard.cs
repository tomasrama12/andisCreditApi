using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditCardApi.Enums;

namespace CreditCardApi.DTOs.Interfaces
{
    public interface ICard
    {
        long CardId {get; set;}
        string OwnerFirstName {get; set;}
        string OwnerLastName {get; set;}
        string OwnerId {get; set;}
        CardType CardType {get; set;}

    }
}