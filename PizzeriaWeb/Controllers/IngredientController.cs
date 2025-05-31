using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Core.Interfaces;
using static Pizzeria.Domain.DTO.IngredientDTO;

namespace Pizzeria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class IngredientController : ControllerBase
    {

        public readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpPost]
        public async Task<IActionResult> AddIngredient(AddIngredientDTO ingredient)
        {

            if (ingredient == null)
                return BadRequest("Obligatory ingredient data");

            try
            {
                await _ingredientService.AddIngredient(ingredient);
                return Ok("Ingredient added");
            }
            catch (ArgumentException Ex)
            {
                return NotFound(Ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIngredient(UpdateIngredientDTO ingredient)
        {

            if (ingredient == null)
                return BadRequest("Obligatory ingredient data");

            try
            {

                await _ingredientService.UpdateIngredient(ingredient);
                return Ok("Ingredient uppdated");
            }
            catch (ArgumentException Ex)
            {
                return NotFound(Ex.Message);
            }
        }
    }
}

