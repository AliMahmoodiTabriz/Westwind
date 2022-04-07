using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcClient.Concrete;
using GRPCSample;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrpcClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrpcProductTestController : ControllerBase
    {
        private readonly ProductServiceManager _serviceManager;

        public GrpcProductTestController(ProductServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost(template: "setcount")]
        public async Task<IActionResult> SetCount(ProductCount productCount)
        {
            return Ok(await _serviceManager.SetProductUnaryCount(productCount));
        }
        [HttpPost(template: "getcount")]
        public async Task<IActionResult> GetCount(ProductRequest productRequest)
        {
            return Ok(await _serviceManager.GetProductUnaryCount(productRequest));
        }
        [HttpPost(template: "serverstreamhello")]
        public async Task<IActionResult> ServerStreamHello(int duration)
        {
            await _serviceManager.ServerStreamHello(duration);
            return Ok();
        }
        [HttpPost(template: "clientstreamhello")]
        public async Task<IActionResult> ClientStreamHello(int duration)
        {
            await _serviceManager.ClientStreamHello(duration);
            return Ok();
        }
        [HttpPost(template: "bidirectionalstreamhello")]
        public async Task<IActionResult> BiDirectionalStreamHello(int duration)
        {
            await _serviceManager.BiDirectionalStreamHello(duration);
            return Ok();
        }
    }
}