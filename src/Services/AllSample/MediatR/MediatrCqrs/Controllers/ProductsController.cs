using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MediatrCqrs.Application.MediatR.Command;
using MediatrCqrs.Application.MediatR.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediatrCqrs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(template: "getall")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllProductQuery()));
        }
        
        [HttpPost(template: "changeprice")]
        public async Task<IActionResult> ChangePrice(ProductPriceChangeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost(template: "changecount")]
        public async Task<IActionResult> ChangeCount(ProductCountChangeCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }
    }
}
