using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Entities
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [Required]
        [StringLength(100)]
        public string IngredientName { get; set; }
        public ICollection<Item> Products { get; set; } = new List<Item>();

    }
}
