using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditCardApi.DataAccess.Models;
using CreditCardApi.DTOs;
using CreditCardApi.Models;

namespace CreditCardApi.DataAccess.Repositories
{
    public interface ICreditCardRepository
    {
        Task<List<CreditCard>> GetCreditCardsAsync();
        Task<CreditCard> PostCreditCardByIdAsync();
        Task<CreditCard> GetCreditCardByIdAsync(long id);
        Task<Movement> PostMovement(long id, MovementDTO movement);
        Task<Movement> GetMovementById(long movementId);
    }
}