using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditCardApi.DataAccess.Repositories;
using CreditCardApi.DTOs;
using CreditCardApi.DTOs.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardApi.Controllers
{
    [ApiController]
    [Route("api/v1/cards")]
    public class CreditCardController : ControllerBase
    {
        private ICreditCardRepository creditCardRepository = null;

        public CreditCardController(ICreditCardRepository creditCardRepo){
            this.creditCardRepository = creditCardRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
           return Ok(await this.creditCardRepository.GetCreditCardsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> PostCard()
        {
            return Ok(await this.creditCardRepository.PostCreditCardByIdAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardById(long id)
        {
            return Ok(await this.creditCardRepository.GetCreditCardByIdAsync(id));
        }

        [HttpPost("{id}/movements")]
        public async Task<IActionResult> PostMovement(long id, MovementDTO movement)
        {
            
            return Ok(await this.creditCardRepository.PostMovement(id, movement));
        }

        [HttpGet("/movements/{movementId}")]
        public async Task<IActionResult> GetMovementById(long movementId)
        {
            return Ok(await this.creditCardRepository.GetMovementById(movementId));
        }
    }
}