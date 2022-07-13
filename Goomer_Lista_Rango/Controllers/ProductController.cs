using AutoMapper;
using Goomer_Lista_Rango.Data;
using Goomer_Lista_Rango.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Goomer_Lista_Rango.Controllers;
[ApiController]
[Route("{controller}")]
public class ProductController : ControllerBase
{
    private IWebHostEnvironment _environment;
    private RestaurantContext _context;
    public ProductController(RestaurantContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [HttpPost]
    public IActionResult AddProduct([FromForm] CreateProductDTO productDto)
    {
        try
        {
            if (productDto.ProductPhoto.Length > 0)
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\Images\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Images\\");
                }

                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Images\\" + productDto.ProductPhoto.FileName))
                {
                    productDto.ProductPhoto.CopyTo(fileStream);
                    fileStream.Flush();

                    Product newProduct = new();
                    newProduct.Name = productDto.Name;
                    newProduct.Category = productDto.Category;
                    newProduct.Price = productDto.Price;
                    newProduct.ProductPhoto = "\\Images\\" + productDto.ProductPhoto.FileName;
                    newProduct.RestaurantId = productDto.RestaurantId;

                    _context.Products.Add(newProduct);
                    _context.SaveChanges();
                    return CreatedAtAction(nameof(GetProductById), 
                        new { Id = newProduct.ProductId }, newProduct);
                }
            }

            return BadRequest();

        }
        catch (Exception e)
        {
            throw new IOException();
        }
    }
    
    
    
    [HttpGet]
    public IEnumerable<Product> GetAllProducts()
    {
        return _context.Products
            .Include(pr => pr.Promotions)
            .ThenInclude(d => d.Discount)
            .ToList();
    }
    
    

    [HttpGet("Get/{id:int}")]
    public IActionResult GetProductById(int id)
    {
        var productDto = _context.Products
            .Where(i => i.ProductId == id)
            .Select(c => new ReadProductDTO()
            {
                ProductId = c.ProductId,
                Name = c.Name,
                Category = c.Category,
                Price = c.Price,
                ProductPhoto = c.ProductPhoto,
            })
            .FirstOrDefault();
        if (productDto == null)
            return NotFound();
        
        return Ok(productDto);
    }
    
    

    [HttpPut("Update/{id:int}")]
    public IActionResult UpdateProduct(int id, [FromForm] UpdateProductDTO productDto)
    {
        var product = _context.Products.Where(x => x.ProductId == id).FirstOrDefault();
        if (product == null)
            return NotFound();

        try
        {
            if (productDto.ProductPhoto.Length > 0)
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\Images\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Images\\");
                }

                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Images\\"))
                {
                    productDto.ProductPhoto.CopyTo(fileStream);
                    fileStream.Flush();

                    product.Name = productDto.Name;
                    product.Category = productDto.Category;
                    product.Price = productDto.Price;
                    product.ProductPhoto = productDto.ProductPhoto.FileName;
                    product.RestaurantId = productDto.RestaurantId;

                    _context.Products.Update(product);
                    _context.SaveChanges();
                    return NoContent();
                }
            }
            
            return BadRequest();
        }
        catch (Exception e)
        {
            throw new IOException();
        }
    }

    
    
    [HttpDelete("Delete/{id:int}")]
    public IActionResult DeleteProduct(int id)
    {
        var productEntity = _context.Products.Where(x => x.ProductId == id).FirstOrDefault();
        if (productEntity == null)
            return NotFound();

        _context.Products.Remove(productEntity);
        _context.SaveChanges();
        return NoContent();
    }
}