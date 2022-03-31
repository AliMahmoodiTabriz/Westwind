using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonEvent.Events;
using EventBus.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RabbitStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private readonly IEventBus _eventBus;

        public PricesController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpPost(template: "firepricechange")]
        public async Task<IActionResult> FirePriceChange(decimal price)
        {
            _eventBus.Publish(new ProductPriceChangeEvent(price));
            return Ok("Success");
        }
    }
}