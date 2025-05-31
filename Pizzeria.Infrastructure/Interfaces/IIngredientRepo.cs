using static Pizzeria.Domain.DTO.IngredientDTO;

namespace Pizzeria.Infrastructure.Interfaces
{
    public interface IIngredientRepo
    {
        Task AddIngredient(AddIngredientDTO ingredient);
        Task UpdateIngredient(UpdateIngredientDTO ingredient);
    }
}

