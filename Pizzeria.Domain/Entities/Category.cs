using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Entities
{
    public class Category
    {

        [Key]
        public int CategoryId { get; set; }
        [StringLength(100)]
        [Required]
        public string CategoryName { get; set; }
        public List<Item> Items { get; set; }
    }
}
