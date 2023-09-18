using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CreditCardApi.DataAccess.Models;
using CreditCardApi.DTOs;
using CreditCardApi.Models;
using DnsClient.Protocol;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace CreditCardApi.DataAccess.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly DBCreditCardMock creditCardMock;
        public CreditCardRepository(DBCreditCardMock dBCreditCardMock)
        {
            this.creditCardMock = dBCreditCardMock;
        }
        public async Task<List<CreditCard>> GetCreditCardsAsync()
        {
            return await creditCardMock.CreditCards.ToListAsync();
        }

        public async Task<CreditCard> PostCreditCardByIdAsync()
        {
            Random rand = new Random();
            long cardId = rand.Next(100000000, 999999999);

            var c = new CreditCard(cardId, "test", "test", "test", Enums.CardType.Black, 10000.0f, 0.0f);
            await creditCardMock.CreditCards.AddAsync(c);
            await creditCardMock.SaveChangesAsync();
            return c;
        }

        public async Task<CreditCard> GetCreditCardByIdAsync(long id)
        {
            CreditCard card = await creditCardMock.CreditCards.FindAsync(id);
            return card;
        }

        public async Task<Movement> PostMovement(long id, MovementDTO movement)
        {
            CreditCard card = await this.GetCreditCardByIdAsync(id);

            if (movement.Amount < 0)
            {
                return null;
            }
            else if (card == null)
            {
                return null;
            }
            Random rand = new Random();
            long movementId = rand.Next(100000000, 999999999);
            var m = new Movement(movement.Amount, DateTime.Now, movement.Description, movement.BusinessName, movement.currency, id, movementId);
            await creditCardMock.Movements.AddAsync(m);
            await creditCardMock.SaveChangesAsync();
            card.AmountSpent += movement.Amount;
            creditCardMock.CreditCards.Update(card);
            await creditCardMock.SaveChangesAsync();
            return m;
        }

        public async Task<Movement> GetMovementById(long movementId)
        {
            Movement movement = await creditCardMock.Movements.FindAsync(movementId);
            return movement;
        }
    }
}