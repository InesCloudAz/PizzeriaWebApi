using Pizzeria.Core.Interfaces;
using Pizzeria.Domain.DTO;
using Pizzeria.Infrastructure.Interfaces;

namespace Pizzeria.Core.Services
{
    public class IngredientService : IIngredientService
    {

        private readonly IIngredientRepo _repo;

        public IngredientService(IIngredientRepo repo)
        {
            _repo = repo;
        }

        public async Task AddIngredient(IngredientDTO.AddIngredientDTO ingredientDTO)
        {
            await _repo.AddIngredient(ingredientDTO);
        }

        public async Task UpdateIngredient(IngredientDTO.UpdateIngredientDTO ingredient)
        {
            await _repo.UpdateIngredient(ingredient);
        }
    }
}
