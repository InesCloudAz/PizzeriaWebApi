using Microsoft.EntityFrameworkCore;
using Pizzeria.Domain.DTO;
using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure.Identities;
using Pizzeria.Infrastructure.Interfaces;

namespace Pizzeria.Infrastructure.Repos
{
    public class ItemRepo : IItemRepo
    {

        private readonly ApplicationUserContext _context;

        public ItemRepo(ApplicationUserContext context)
        {
            _context = context;
        }

        public async Task CreateNewItem(ItemDTO.CreateItemDTO item)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.CategoryId == item.CategoryId);

            var ingredients = await _context.Ingredients
            .Where(i => item.IngredientId.Contains(i.IngredientId))
            .ToListAsync();

            var productDto = new Item
            {
                ItemName = item.ItemName,
                Price = item.Price,
                Category = category,
                Ingredients = ingredients
            };
            _context.Items.Add(productDto);
            _context.SaveChangesAsync();

        }

        public async Task UpdateItem(ItemDTO.UpdateItemDTO item)
        {
            var currentItem = await _context.Items
                .Include(p => p.Ingredients)
                .SingleOrDefaultAsync(p => p.ItemId == item.ItemId);

            if (item.ItemName != null)
                currentItem.ItemName = item.ItemName;

            if (item.Price.HasValue)
                currentItem.Price = item.Price.Value;

            if (item.CategoryId.HasValue)
            {
                var category = await _context.Categories
                    .SingleOrDefaultAsync(c => c.CategoryId == item.CategoryId.Value);
                currentItem.Category = category;
            }

            if (item.ItemId != null)
            {
                var newIngredients = await _context.Ingredients
                    .Where(i => item.IngredientId.Contains(i.IngredientId))
                    .ToListAsync();
                currentItem.Ingredients.Clear();
                foreach (var ingredient in newIngredients)
                    currentItem.Ingredients.Add(ingredient);
            }

            await _context.SaveChangesAsync();
        }
    }
}

