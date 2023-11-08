using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditCardApi.DataAccess.Repositories;
using CreditCardApi.DTOs;
using CreditCardApi.DTOs.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CreditCardApi.Controllers
{
    [ApiController]
    [Route("api/v1/cards")]
    
    public class CreditCardController : ControllerBase
    {
        private readonly ILogger<CreditCardController> logger;
        private ICreditCardRepository creditCardRepository = null;
        public CreditCardController(ILogger<CreditCardController> logger, ICreditCardRepository creditCardRepo){
            this.creditCardRepository = creditCardRepo;
            this.logger = logger;
        }   

        async Task<List<Models.CreditCard>> HandleGet1([FromServices]ILogger<Program> logger)
            {
                var result = await this.creditCardRepository.GetCreditCardsAsync();
                logger.LogInformation("Anduvo");
                return result;
            }

        [EnableRateLimiting("SlidingWindow")]
        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            return Ok(this.creditCardRepository.GetCreditCardsAsync());
        }
        [EnableRateLimiting("Concurrency")]
        [HttpPost]
        public async Task<IActionResult> PostCard()
        {
            return Ok(await this.creditCardRepository.PostCreditCardByIdAsync());
        }

        [EnableRateLimiting("FixedWindow")]
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

        [EnableRateLimiting("TokenBucket")]
        [HttpGet("/movements/{movementId}")]
        public async Task<IActionResult> GetMovementById(long movementId)
        {
            return Ok(await this.creditCardRepository.GetMovementById(movementId));
        }
    }
}