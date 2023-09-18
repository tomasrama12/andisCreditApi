using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardApi.DTOs.Interfaces
{
    public interface IMovement
    {
        float Amount { get; set; }
        DateTime Date { get; set; }
        string Description { get; set; }
        string BusinessName { get; set; }
        string currency { get; set; }
    }
}