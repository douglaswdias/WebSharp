using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSharp.Api.Data;
using WebSharp.Api.Dtos;
using WebSharp.Api.Models;

namespace WebSharp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoryController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Category>> Post(CreateCategoryDto createCategoryDto)
    {
        var categoryEntity = new Category()
        {
            Name = createCategoryDto.Name,
            Description = createCategoryDto.Description
        };
        await _context.AddAsync(categoryEntity);
        await _context.SaveChangesAsync();
        
        return Ok(categoryEntity);
    }

    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetAllCategories()
    {
        var categories = await _context.Categories.ToListAsync();
        
        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Category>> GetCategoryById(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        
        return category is null ? NotFound("Category not found") : Ok(category);
    }

    [HttpPut]
    public async Task<ActionResult<Category>> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        var dbCategory = await _context.Categories.FindAsync(updateCategoryDto.Id);
        if (dbCategory is null) 
            return NotFound("Category not found");

        var categoryEntity = new Category()
        {
            Name = updateCategoryDto.Name,
            Description = updateCategoryDto.Description
        };
        
        dbCategory.Name = categoryEntity.Name;
        dbCategory.Description = categoryEntity.Description;
        await _context.SaveChangesAsync();
        
        return Ok(categoryEntity);
        
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Category>> DeleteCategory(int id)
    {
        var dbCategory = await _context.Categories.FindAsync(id);
        if (dbCategory is null)
            return NotFound("Category not found");
        
        _context.Categories.Remove(dbCategory);
        await _context.SaveChangesAsync();
        
        return Ok(dbCategory);
    }

}