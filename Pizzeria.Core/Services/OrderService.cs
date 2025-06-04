using Pizzeria.Core.DTO;
using Pizzeria.Core.Interfaces;
using Pizzeria.Infrastructure.Interfaces;

namespace Pizzeria.Core.Services
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepo _repo;

        public OrderService(IOrderRepo repo)
        {
            _repo = repo;
        }

        public async Task<int> AddOrder(OrderDTO.AddOrderDTO orderDTO, string userId)
        {
            return await _repo.AddOrder(orderDTO, userId);
        }

        public async Task<int> AddPremiumOrder(OrderDTO.AddOrderDTO orderDTO, string userId)
        {
            return await _repo.AddPremiumOrder(orderDTO, userId);
        }

        public async Task DeleteOrder(int orderID)
        {
            await _repo.DeleteOrder(orderID);
        }

        public async Task<List<OrderDTO.GetOrderDTO>> GetAuthUserOrders(string userId)
        {
            return await _repo.GetAuthUserOrders(userId);
        }

        public async Task UpdateOrderStatus(int orderID, bool delivered)
        {
            await _repo.UpdateOrderStatus(orderID, delivered);
        }

        
    }
}
