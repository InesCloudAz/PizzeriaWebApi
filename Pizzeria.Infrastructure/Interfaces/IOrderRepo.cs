using static Pizzeria.Core.DTO.OrderDTO;

namespace Pizzeria.Infrastructure.Interfaces
{
    public interface IOrderRepo
    {
        Task<int> AddOrder(AddOrderDTO orderDto, string userId);
        Task<int> AddPremiumOrder(AddOrderDTO orderDto, string userId);
        Task<List<GetOrderDTO>> GetAuthUserOrders(string userId);
        Task DeleteOrder(int orderId);
        Task UpdateOrderStatus(int orderId, bool isDelivered);
    }
}

