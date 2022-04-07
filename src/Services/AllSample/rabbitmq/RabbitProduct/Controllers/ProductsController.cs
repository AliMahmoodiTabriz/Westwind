using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonEvent.Events;
using EventBus.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RabbitProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IEventBus _eventBus;

        public ProductsController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpPost(template: "fireproductcount")]
        public async Task<IActionResult> FireProductCount(int count)
        {
            _eventBus.Publish(new ProductCountChangeEvent(count));
            return Ok("success");
        }
        
        [HttpPost(template: "test")]
        public async Task<IActionResult> Test(string message)
        {
            _eventBus.Publish(new HellowordEvent(message));
            return Ok("success");
        }
    }
}