using Microsoft.AspNetCore.Mvc;

namespace Goomer_Lista_Rango.Controllers;

[ApiController]
[Route("{controller}")]
public class OpenHourController : Controller
{
    private RestaurantContext _context;
    public OpenHourController(RestaurantContext context)
    {
        _context = context;
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteHour(int id)
    {
        var list = _context.OpenHours.ToList().FindAll(i => i.RestaurantId == id);

        _context.OpenHours.RemoveRange(list);
        _context.SaveChanges();
        return NoContent();
    }
    
}