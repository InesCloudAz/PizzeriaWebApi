using static Pizzeria.Domain.DTO.ItemDTO;

namespace Pizzeria.Infrastructure.Interfaces
{
    public interface IItemRepo
    {
        Task CreateNewItem(CreateItemDTO item);
        Task UpdateItem(UpdateItemDTO item);
    }
}
