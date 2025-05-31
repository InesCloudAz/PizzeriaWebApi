using static Pizzeria.Domain.DTO.ItemDTO;

namespace Pizzeria.Core.DTO
{
    public class OrderDTO
    {

        public class AddOrderDTO
        {
            public List<int> ItemId { get; set; } = new List<int>();
        }

        public class GetOrderDTO
        {
            public int Price { get; set; }
            public ICollection<GetItemDTO> Items { get; set; } = new List<GetItemDTO>();
        }
    }
}
