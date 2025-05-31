namespace Pizzeria.Domain.DTO
{
    public class IngredientDTO
    {

        public class AddIngredientDTO
        {
            public string Name { get; set; }
        }

        public class UpdateIngredientDTO
        {
            public int IngredientId { get; set; }
            public string Name { get; set; }
        }
    }
}
