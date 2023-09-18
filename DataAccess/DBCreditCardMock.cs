using CreditCardApi.DataAccess.Models;
using CreditCardApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditCardApi.DataAccess
{
    public class DBCreditCardMock: DbContext
    {
        public DBCreditCardMock(DbContextOptions<DBCreditCardMock> options) : base(options)
        {
        }
        public DbSet<CreditCard> CreditCards { get; set; } = null!;
        public DbSet<Movement> Movements { get; set; } = null!;
    }
}