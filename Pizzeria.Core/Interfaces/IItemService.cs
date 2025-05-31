using static Pizzeria.Domain.DTO.ItemDTO;

namespace Pizzeria.Core.Interfaces
{
    public interface IItemService
    {
        Task CreateItem(CreateItemDTO item);
        Task UpdateItem(UpdateItemDTO item);
    }
}
