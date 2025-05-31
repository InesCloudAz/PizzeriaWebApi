using Pizzeria.Core.Interfaces;
using Pizzeria.Domain.DTO;
using Pizzeria.Infrastructure.Interfaces;

namespace Pizzeria.Core.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepo _repo;

        public ItemService(IItemRepo repo)
        {
            _repo = repo;
        }

        public async Task CreateItem(ItemDTO.CreateItemDTO item)
        {
            await _repo.CreateNewItem(item);
        }

        public async Task UpdateItem(ItemDTO.UpdateItemDTO item)
        {
            await _repo.UpdateItem(item);
        }
    }
}
