using static Pizzeria.Core.DTO.OrderDTO;

namespace Pizzeria.Infrastructure.Interfaces
{
    public interface IOrderRepo
    {
        Task AddOrder(AddOrderDTO orderDto, string userId);
        Task AddPremiumOrder(AddOrderDTO orderDto, string userId);
        Task<List<GetOrderDTO>> GetAuthUserOrders(string userId);
        Task DeleteOrder(int orderId);
        Task UpdateOrderStatus(int orderId, bool isDelivered);
    }
}

