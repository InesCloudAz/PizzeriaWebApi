namespace Pizzeria.Domain.DTO
{
    public class ItemDTO
    {
        public class GetItemDTO
        {
            public string ItemName { get; set; }
            public int Price { get; set; }
        }

        public class CreateItemDTO
        {
            public string ItemName { get; set; }
            public int Price { get; set; }
            public int CategoryId { get; set; }
            public List<int> IngredientId { get; set; }
        }

        public class UpdateItemDTO
        {
            public int ItemId { get; set; }
            public string? ItemName { get; set; }
            public int? Price { get; set; }
            public int? CategoryId { get; set; }
            public List<int>? IngredientId { get; set; }
        }
    }
}




