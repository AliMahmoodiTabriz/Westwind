using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisCache.Application.Abstract;
using RedisCache.Domian;

namespace RedisCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost(template: "add")]
        public async Task<IActionResult> Add(Product product)
        {
            return Ok(await _productService.Add(product));
        }

        [HttpGet(template: "getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpGet(template: "getbycategory")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            return Ok(await _productService.GetByCategory(category));
        }

        [HttpGet(template: "getbyid")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _productService.GetById(id));
        }

        [HttpGet(template: "getbyname")]
        public async Task<IActionResult> GetByName(string name)
        {
            return Ok(await _productService.GetByName(name));
        }

        [HttpGet(template: "removeall")]
        public async Task<IActionResult> RemoveAll()
        {
            await _productService.RemoveAll();
            return Ok();
        }
    }
}