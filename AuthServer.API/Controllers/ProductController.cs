using AuthServer.Core.Dtos;
using AuthServer.Core.Entity;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace AuthServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ProductController : CustomBaseController
{
    private readonly IGenericService<Product,ProductDto> _productService;

    public ProductController(IGenericService<Product, ProductDto> productSerive)
    {
        _productService = productSerive;
    }

    [HttpGet]
    
    public async Task<IActionResult> GetProducts()
    {
        return ActionResultInstance(await _productService.GetAllAsync());
    }

    [HttpPost]

    public async Task<IActionResult> SaveProducts(ProductDto productDto)
    {
        return ActionResultInstance(await _productService.AddAsync(productDto));
    }

    [HttpPut]

    public async Task<IActionResult> UpdateProduct(ProductDto productDto)
    {
        return ActionResultInstance(await _productService.Update(productDto, productDto.Id));
    }

    [HttpDelete]

    public async Task<IActionResult> RemoveProduct(int id)
    {
        return ActionResultInstance(await _productService.Remove(id));
    }
}
