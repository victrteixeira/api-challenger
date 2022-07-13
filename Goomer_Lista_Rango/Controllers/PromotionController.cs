using System.Globalization;
using AutoMapper;
using Goomer_Lista_Rango.Data;
using Goomer_Lista_Rango.DTOs.Promotion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Goomer_Lista_Rango.Controllers;

[ApiController]
[Route("{controller}")]
public class PromotionController : ControllerBase
{
    private readonly RestaurantContext _context;

    public PromotionController(RestaurantContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddPromotion([FromBody] CreatePromotionDTO promotionDto)
    {
        Promotion newPromotion = new();
        newPromotion.DiscountId = promotionDto.DiscountId;
        newPromotion.ProductId = promotionDto.ProductId;
        newPromotion.PromotionDescription = promotionDto.PromotionDescription;

        DateTime promotionBegin;
        var parseBegin = DateTime.TryParseExact(promotionDto.PromotionBegins, @"yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out promotionBegin);
        if (!parseBegin)
            throw new FormatException("String isn't in correct format");
        
        DateTime promotionEnd;
        var parseEnd = DateTime.TryParseExact(promotionDto.PromotionEnds, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out promotionEnd);
        if (!parseEnd)
            throw new FormatException("String isn't in correct format");

        DateTime.Compare(promotionBegin, promotionEnd);
        if (promotionEnd < promotionBegin)
            throw new ArgumentException("It's no possible to insert a promotion which the begin isn't before the end.");
        

        newPromotion.PromotionBegins = promotionBegin;
        newPromotion.PromotionEnds = promotionEnd;

        _context.Promotions.Add(newPromotion);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetPromotionById), new { Id = newPromotion.ProductId }, newPromotion);
    }

    [HttpGet("Get/{id}")]
    public IActionResult GetPromotionById(int id)
    {
        var promotionDto = _context.Promotions.Where(i => i.ProductId == id)
            .Include(d => d.Product)
            .Include(pr => pr.Discount)
            .Select(c => new ReadPromotionDTO()
            {
                Discount = c.Discount,
                PromotionBegins = c.PromotionBegins,
                PromotionEnds = c.PromotionEnds,
                PromotionDescription = c.PromotionDescription
            })
            .FirstOrDefault();
        if (promotionDto == null)
            return NotFound();
        
        return Ok(promotionDto);
    }

    [HttpGet]
    public IEnumerable<Promotion> GetAllPromotions()
    {
        return _context.Promotions
            .Include(d => d.Discount)
            .Include(pr => pr.Product)
            .ToList();
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeletePromotion(int id)
    {
        var promotionEntity = _context.Promotions.Where(x => x.ProductId == id).FirstOrDefault();
        if (promotionEntity == null)
            return NotFound();

        _context.Promotions.Remove(promotionEntity);
        _context.SaveChanges();
        return NoContent();
    }
}

// TODO > Test interactivity between Product Class -> Discount Class between Promotion Class intermediate. And then API it's done.