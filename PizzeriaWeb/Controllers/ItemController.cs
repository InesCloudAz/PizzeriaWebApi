using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Core.Interfaces;
using static Pizzeria.Domain.DTO.ItemDTO;

namespace Pizzeria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ItemController : ControllerBase
    {

        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(CreateItemDTO item)
        {

            if (item == null)
                return BadRequest("You must provide item data");

            try
            {
                await _itemService.CreateItem(item);
                return Ok("Item created");
            }
            catch (ArgumentException Ex)
            {
                return NotFound(Ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItem(UpdateItemDTO item)
        {

            if (item == null)
                return BadRequest("You must provide item data");

            try
            {
                await _itemService.UpdateItem(item);
                return Ok("Item updated");
            }
            catch (ArgumentException Ex)
            {
                return NotFound(Ex.Message);
            }
        }
    }
}
