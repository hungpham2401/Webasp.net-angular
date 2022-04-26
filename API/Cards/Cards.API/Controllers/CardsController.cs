using Cards.API.Data;
using Cards.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardsDbContext cardsDbContext;

        public CardsController(CardsDbContext cardsDbContext)
        {
            this.cardsDbContext = cardsDbContext;
        }


        //Get All Cards 
        [HttpGet]
        public async  Task<IActionResult> GetAllCards()
        {
            var cards = await cardsDbContext.Cards.ToListAsync();
            return Ok(cards);

        }

        //Get Single Cards 
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var cards = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if(cards != null)
            {
                return Ok(cards);
            }
            else
            {
                return NotFound("Card not Found ('Khong the tim thay Card')");
            }
        }
        // Add Cards
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        
        {
            card.Id = Guid.NewGuid();
            await cardsDbContext.Cards.AddAsync(card);
            await cardsDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCard), new { id = card.Id } , card);

        }

        //Update Cards 
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard ([FromRoute] Guid id , [FromBody] Card card)
        {
            var ExistingCard = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if(ExistingCard != null)
            {
                ExistingCard.CarddholderName = card.CarddholderName;
                ExistingCard.CardNumber = card.CardNumber;
                ExistingCard.ExpiryMonth = card.ExpiryMonth;
                ExistingCard.ExpiryYear = card.ExpiryYear;
                ExistingCard.CVC= card.CVC;
                await cardsDbContext.SaveChangesAsync();
                return Ok(ExistingCard);
            }   
            else
            {
                return NotFound("Card not Found ('Khong the tim thay Card')");
            }
        }
        //Delet Cards 
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard ([FromRoute] Guid id )
        {
            var existingCard = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                cardsDbContext.Cards.Remove(existingCard);
                await cardsDbContext.SaveChangesAsync();
                return Ok(existingCard);
            }
            else
            {
                return NotFound("Card not Found ('Khong the tim thay Card')");
            }
        }
    }
}
