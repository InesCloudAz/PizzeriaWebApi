using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Core.DTO;
using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure.Identities;
using Pizzeria.Infrastructure.Interfaces;
using static Pizzeria.Core.DTO.OrderDTO;
using static Pizzeria.Domain.DTO.ItemDTO;

namespace Pizzeria.Infrastructure.Repos
{
    public class OrderRepo : IOrderRepo
    {

        private readonly ApplicationUserContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderRepo(ApplicationUserContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task <int> AddOrder(OrderDTO.AddOrderDTO orderDto, string userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                throw new ArgumentException($"No user found with ID {userId}");

            var items = await _context.Items
                .Where(i => orderDto.ItemId.Contains(i.ItemId))
                .ToListAsync();

            var totalCost = items.Sum(i => i.Price);

            var order = new Order
            {
                User = user,
                Items = items,
                TotalPrice = totalCost,

            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return user.BonusPoints;
        }

        public async Task<int> AddPremiumOrder(OrderDTO.AddOrderDTO orderDto, string userId)
        {


            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new ArgumentException($"No user found with ID {userId}");

            var items = await _context.Items
                .Where(p => orderDto.ItemId.Contains(p.ItemId))
                .ToListAsync();

            var entireItems = items.Count;
            var fullPrice = items.Sum(p => p.Price);

            if (user.BonusPoints >= 100 && items.Any())
            {
                var baseItemPrice = items.Min(p => p.Price);
                fullPrice -= baseItemPrice;
                user.BonusPoints -= 100;
            }

            if (entireItems >= 3)
            {
                fullPrice = (int)(fullPrice * 0.8);
            }

            if (fullPrice < 0) fullPrice = 0;

           

            user.BonusPoints += entireItems * 10;

           

            var order = new Order
            {
                User = user,
                Items = items,
                TotalPrice = fullPrice,
                DeliveryStatus = false,

            };


            await _userManager.UpdateAsync(user);

            await _context.Orders.AddAsync(order);

            await _context.SaveChangesAsync();

            return user.BonusPoints;
        }

        public async Task<List<OrderDTO.GetOrderDTO>> GetAuthUserOrders(string userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new ArgumentException($"No user found with ID {userId}");

            var orders = await _context.Orders
                .Where(o => o.User.Id == userId)
                .Include(o => o.Items)
                .Include(o => o.User)
                .AsNoTracking()
                .ToListAsync();

            var orderDto = orders.Select(o => new GetOrderDTO
            {
                Price = o.TotalPrice,
                Items = o.Items.Select(p => new GetItemDTO
                {
                    ItemName = p.ItemName,
                    Price = p.Price
                }).ToList(),

                BonusPoints = o.User.BonusPoints
            }).ToList();

            return orderDto;
        }

        public async Task DeleteOrder(int orderId)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
                throw new ArgumentException($"No order found with ID {orderId}");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderStatus(int orderId, bool isDelivered)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
                throw new ArgumentException($"No order found with ID {orderId}");

            order.DeliveryStatus = isDelivered;
            await _context.SaveChangesAsync();

        }
    }
}
