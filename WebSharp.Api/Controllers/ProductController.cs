using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSharp.Api.Data;
using WebSharp.Api.Dtos;
using WebSharp.Api.Models;

namespace WebSharp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(CreateProductDto createProductDto)
    {
        var productEntity = new Product()
        {
            Name = createProductDto.Name,
            Description = createProductDto.Description,
            Price = createProductDto.Price,
            CategoryId = createProductDto.CategoryId,
        };
        
        await _context.Products.AddAsync(productEntity);
        await _context.SaveChangesAsync();
        
        return Ok(productEntity);
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAllProducts()
    {
        var products = await _context.Products.ToListAsync();
        
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
            return NotFound("Product not found");
        
        return Ok(product);
    }

    [HttpPut]
    public async Task<ActionResult<Product>> UpdateProduct(UpdateProductDto updateProductDto)
    {
        var productDb = await _context.Products.FindAsync(updateProductDto.Id);
        if (productDb is null)
            return NotFound("Product not found");

        var productEntity = new Product()
        {
            Name = updateProductDto.Name,
            Description = updateProductDto.Description,
            Price = updateProductDto.Price,
            CategoryId = updateProductDto.CategoryId
        };
        
        productDb.Name = updateProductDto.Name;
        productDb.Description = updateProductDto.Description;
        productDb.Price = updateProductDto.Price;
        productDb.CategoryId = updateProductDto.CategoryId;
        
        await _context.SaveChangesAsync();
        return Ok(productEntity);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
            return NotFound("Product not found");
        
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        
        return Ok(product);
    }
}