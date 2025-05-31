using System.ComponentModel.DataAnnotations;


namespace Pizzeria.Domain.Entities
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        [StringLength(100)]
        public string ItemName { get; set; }
        public int Price { get; set; }
        public Category Category { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}


