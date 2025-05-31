using static Pizzeria.Domain.DTO.IngredientDTO;

namespace Pizzeria.Core.Interfaces
{
    public interface IIngredientService
    {
        Task AddIngredient(AddIngredientDTO ingredientDTO);
        Task UpdateIngredient(UpdateIngredientDTO ingredient);
    }
}
