using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditCardApi.DataAccess.Repositories;
using CreditCardApi.DTOs;
using CreditCardApi.DTOs.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;

using CreditCardApi.Logger;
using System;
using System.IO;
using Microsoft.AspNetCore.OutputCaching;


namespace CreditCardApi.Controllers
{
    [ApiController]
    [Route("api/v1/cards")]
    public class CreditCardController : ControllerBase
    {
        private readonly IOutputCacheStore _cache;
        private Serilog.Core.Logger logger = Logger.Logger.createLogger();
        private Serilog.Core.Logger fileLogger = Logger.Logger.createFileLogger(); 
        //private Serilog.Core.Logger SQLiteLogger = Logger.Logger.createSQLite(); 

        private ICreditCardRepository creditCardRepository = null;
        public CreditCardController(ICreditCardRepository creditCardRepo, IOutputCacheStore cache){
            this.creditCardRepository = creditCardRepo;
             _cache = cache;
        }   


        [EnableRateLimiting("SlidingWindow")]
        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            logger.Information("GetCards: ");
            var cards = await this.creditCardRepository.GetCreditCardsAsync();
            logger.Information(cards.Count().ToString());
            //SQLiteLogger.Information("Cantidad de cartas: " + cards.Count().ToString());
            return Ok(cards);
        }
        [EnableRateLimiting("Concurrency")]
        [HttpPost]
        public async Task<IActionResult> PostCard()
        {
            return Ok(await this.creditCardRepository.PostCreditCardByIdAsync());
        }

        //[EnableRateLimiting("FixedWindow")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardById(long id)
        {
            //logger.Information("GetCardId: " + id);
            //fileLogger.Information("GetCardsId: " + id);
            var card = await this.creditCardRepository.GetCreditCardByIdAsync(id);
            //fileLogger.Information("Card with id: " + id);
            try{
                throw new NullReferenceException("Esto es un error.");
            }
            catch(NullReferenceException e){
                fileLogger.Error(e.Message);
            }
            return Ok(card);
        
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

        //[OutputCache(PolicyName = "PicturePolicy")]
        [HttpGet("/pictures/{pictureId}")]
        public string getPicture(long id)
        {
            if (System.IO.File.Exists("pictures/base64.txt"))
            {
                // Lee el contenido del archivo y almacénalo en una cadena
                string fileContent = System.IO.File.ReadAllText("pictures/base64.txt");
                return fileContent;
            }

            return null;
        }
    }
}