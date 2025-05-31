using Microsoft.EntityFrameworkCore;
using Pizzeria.Domain.DTO;
using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure.Identities;
using Pizzeria.Infrastructure.Interfaces;

namespace Pizzeria.Infrastructure.Repos
{
    public class IngredientRepo : IIngredientRepo
    {

        private readonly ApplicationUserContext _context;

        public IngredientRepo(ApplicationUserContext context)
        {
            _context = context;
        }

        public async Task AddIngredient(IngredientDTO.AddIngredientDTO ingredient)
        {
            var ingredientDTO = new Ingredient
            {
                IngredientName = ingredient.Name,

            };

            await _context.Ingredients.AddAsync(ingredientDTO);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateIngredient(IngredientDTO.UpdateIngredientDTO ingredient)
        {
            var currentIngredient = await _context.Ingredients
                 .SingleOrDefaultAsync(i => i.IngredientId == ingredient.IngredientId);

            if (currentIngredient == null)
                throw new ArgumentException($"No ingredient found with ID {ingredient.IngredientId}");

            currentIngredient.IngredientName = ingredient.Name;
            await _context.SaveChangesAsync();
        }
    }
}
