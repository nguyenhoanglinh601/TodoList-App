using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo_List.Models;
using Todo_List.Services;

namespace Todo_List.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemsController(ItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public ActionResult<List<Item>> Get()
        {
            if (Authenticate())
            {
                string userId = HttpContext.Session.GetString("userId");
                return _itemService.Get(userId);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id:length(24)}", Name = "GetItem")]
        public ActionResult<Item> GetItem(string id)
        {
            if (!Authenticate())
            {
                return NotFound();
            }

            string userId = HttpContext.Session.GetString("userId");
            var item = _itemService.GetItem(id, userId);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public ActionResult<Item> Create(Item item)
        {
            _itemService.Create(item);

            return CreatedAtRoute("GetItem", new { id = item.Id.ToString() }, item);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Item itemIn)
        {
            if(!Authenticate()){
                return NotFound();
            }
            string userId = HttpContext.Session.GetString("userId");
            var item = _itemService.GetItem(id, userId);

            if (item == null)
            {
                return NotFound();
            }

            _itemService.Update(id, itemIn);

            return NoContent();
        }

        private bool Authenticate()
        {
            //Check HttpContext.Session.getString("userId") != null => return true;
            //else return false;

            //Ignore authenticate:
            HttpContext.Session.SetString("userId", "1");
            return true;
        }
    }
}
