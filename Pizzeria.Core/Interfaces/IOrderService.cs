﻿using static Pizzeria.Core.DTO.OrderDTO;

namespace Pizzeria.Core.Interfaces
{
    public interface IOrderService
    {
        Task<int> AddOrder(AddOrderDTO orderDTO, string userId);
        Task<List<GetOrderDTO>> GetAuthUserOrders(string userId);
        Task<int> AddPremiumOrder(AddOrderDTO orderDTO, string userId);
        Task DeleteOrder(int orderID);
        Task UpdateOrderStatus(int orderID, bool delivered);
    }
}
