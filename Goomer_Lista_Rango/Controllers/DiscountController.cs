using AutoMapper;
using Goomer_Lista_Rango.Data;
using Goomer_Lista_Rango.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Goomer_Lista_Rango.Controllers;

[ApiController]
[Route("{controller}")]
public class DiscountController : ControllerBase
{
    private RestaurantContext _context;
    
    public DiscountController(RestaurantContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public IActionResult AddDiscount([FromBody] CreateDiscountDTO discountDto)
    {
        Discount newDiscount = new();
        newDiscount.DiscountPrice = discountDto.DiscountPrice;

        _context.Discounts.Add(newDiscount);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetDiscountById), new { Id = newDiscount.DiscountId }, newDiscount);
    }
    
    [HttpGet]
    public IEnumerable<Discount> GetAllDiscounts()
    {
        return _context.Discounts
            .ToList();
    }

    [HttpGet("Get/{id:int}")]
    public IActionResult GetDiscountById(int id)
    {
        var discountDto = _context.Discounts.Where(i => i.DiscountId == id)
            .Select(c => new ReadDiscountDTO()
            {
                DiscountId = c.DiscountId,
                DiscountPrice = c.DiscountPrice
            }).FirstOrDefault();
        
        if (discountDto == null)
            return NotFound();

        return Ok(discountDto);
    }

    [HttpPut("Update/{id:int}")]
    public IActionResult UpdateDiscount(int id, [FromBody] UpdateDiscountDTO discountDto)
    {
        var discount = _context.Discounts.Where(i => i.DiscountId == id).FirstOrDefault();
        if (discount == null)
            return NotFound();
        
        discount.DiscountPrice = discountDto.DiscountPrice;

        _context.Discounts.Update(discount);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("Delete/{id:int}")]
    public IActionResult DeleteDiscount(int id)
    {
        var discountEntity = _context.Discounts.Where(i => i.DiscountId == id).FirstOrDefault();
        if (discountEntity == null)
            return NotFound();

        _context.Discounts.Remove(discountEntity);
        _context.SaveChanges();
        return NoContent();
    }
}