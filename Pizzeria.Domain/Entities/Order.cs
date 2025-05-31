using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int TotalPrice { get; set; }
        public bool DeliveryStatus { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
