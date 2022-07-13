using System.Data;
using System.Security.Authentication;
using AutoMapper;
using Goomer_Lista_Rango.Data;
using Goomer_Lista_Rango.DTOs;
using Goomer_Lista_Rango.DTOs.Promotion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Goomer_Lista_Rango.Controllers;
[ApiController]
[Route("{controller}")]
public class RestaurantController : Controller
{
    private static IWebHostEnvironment _environment;
    private RestaurantContext _context;
    public RestaurantController(RestaurantContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }


    [HttpGet] // 1. List all restaurants
    public IEnumerable<Restaurant> GetAllRestaurants()
    {
         var restaurants = _context.Restaurants
            .Include(a => a.Address)
            .Include(p => p.Products)
            .ThenInclude(pr => pr.Promotions)
            .Include(h => h.OpenHours).ToList();
         
         return restaurants;
    }



    [HttpPost] // 2. Create new restaurant
    public IActionResult AddRestaurant([FromForm] CreateRestaurantDTO restaurantDto)
    {
        try
        {
            if (restaurantDto.RestaurantPhoto.Length > 0)
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\Images\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Images\\");
                }
                
                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Images\\" + restaurantDto.RestaurantPhoto.FileName))
                {
                    restaurantDto.RestaurantPhoto.CopyTo(fileStream);
                    fileStream.Flush();

                    Restaurant newRestaurant = new();
                    newRestaurant.Name = restaurantDto.Name;
                    newRestaurant.AddressId = restaurantDto.AddressId;
                    newRestaurant.RestaurantPhoto = "\\Images\\" + restaurantDto.RestaurantPhoto.FileName;
                    
                    _context.Restaurants.Add(newRestaurant);
                    _context.SaveChanges();
                    return CreatedAtAction(nameof(GetRestaurantById),
                        new { Id = newRestaurant.RestaurantId },
                        newRestaurant);
                }
            }
            return BadRequest();
        }
        catch (Exception e)
        {
            throw new IOException();
        }
    }
    
    

    [HttpPut("AddOperationTime/{id:int}")] // -> Test
    public IActionResult AddHourToRestaurant(int id, [FromBody] ListOfDTO openHours)
    {
        bool restaurantExist = _context.Restaurants.Any(i => i.RestaurantId == id);
        if (!restaurantExist)
            throw new ConstraintException();
        
        foreach (var time in openHours.openHours)
        {
            var restaurant = _context.Restaurants.Where(i => i.RestaurantId == id).FirstOrDefault();
            OpenHour newOpenHour = new();
            string[] dayOfWeeks = { @"monday", @"tuesday", @"wednesday", @"thursday", @"friday", @"saturday", @"sunday" };
            if (!dayOfWeeks.Contains(time.DayOfWeek.ToLower()))
                throw new ArgumentException("It's necessary to insert a day between Sunday and Saturday");

            newOpenHour.Day = time.DayOfWeek.ToLower();
            
            TimeOnly parsedOpen;
            bool parsedOpenSuccess = TimeOnly.TryParseExact
            (
                s: time.OpeningTime,
                format: @"HH:mm",
                result: out parsedOpen
            );
            if (parsedOpenSuccess)
                newOpenHour.OpenTime = parsedOpen;
            else
                throw new FormatException("Time invalid, please try to send as aside: HH:mm");
            
            TimeOnly parsedClose;
            bool parsedCloseSuccess = TimeOnly.TryParseExact
            (
                s: time.ClosingTime,
                format: @"HH:mm",
                result: out parsedClose
            );
            if (parsedOpenSuccess)
                newOpenHour.CloseTime = parsedClose;
            else
                throw new FormatException("Time invalid, please try to send as aside: HH:mm");

            newOpenHour.RestaurantId = id;

            _context.OpenHours.Add(newOpenHour);
            _context.SaveChanges();
        }

        return NoContent();
    }
    
    
    
    [HttpGet("Get/{id:int}")] // 3. List restaurant information
    public IActionResult GetRestaurantById(int id)
    {
        var restaurantDto = _context.Restaurants
            .Where(i => i.RestaurantId == id)
            .Include(p => p.Products)
            .ThenInclude(pr => pr.Promotions)
            .ThenInclude(d => d.Discount)
            .Include(h => h.OpenHours)
            .Select(c => new ReadRestaurantDTO()
            {
                RestaurantId = c.RestaurantId,
                Name = c.Name,
                RestaurantPhoto = c.RestaurantPhoto,
                Address = c.Address,
                Product = c.Products,
                OpenHour = c.OpenHours
            }).FirstOrDefault();

        if (restaurantDto == null)
            return NotFound();

        return Ok(restaurantDto);
    }

    
    
    [HttpPut("Update/{id:int}")] // 4. Update restaurant information
    public IActionResult UpdateRestaurant(int id, [FromBody] UpdateRestaurantDTO restaurantDto)
    {
        var restaurant = _context.Restaurants.Where(x => x.RestaurantId == id).FirstOrDefault();
        if (restaurant == null)
            return NotFound();

        try
        {
            if (restaurantDto.RestaurantPhoto.Length > 0)
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\Images\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Images\\");
                }

                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Images\\"))
                {
                    restaurantDto.RestaurantPhoto.CopyTo(fileStream);
                    fileStream.Flush();

                    restaurant.Name = restaurantDto.Name;
                    restaurant.RestaurantPhoto = restaurantDto.RestaurantPhoto.FileName;
                    restaurant.AddressId = restaurantDto.AddressId;

                    _context.Restaurants.Update(restaurant);
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

    
    
    [HttpDelete("Delete/{id:int}")] // 5. Delete a specific restaurant
    public IActionResult DeleteRestaurant(int id)
    {
        var restaurant = _context.Restaurants.Where(x => x.RestaurantId == id).FirstOrDefault();
        if (restaurant == null)
            return NotFound();
        
        _context.Restaurants.Remove(restaurant);
        _context.SaveChanges();
        return NoContent();
    }
    
    
    
    [HttpGet("GetProductFrom")] // 6. To list products from one restaurant
    public IActionResult GetProductFromRestaurant([FromQuery] int restaurantId)
    {
        var products = _context.Products
            .Include(pr => pr.Promotions)
            .ThenInclude(d => d.Discount)
            .ToList().FindAll(i => i.RestaurantId == restaurantId);
        
        return Ok(products);
    }
    
    
    
    
}