using System.Text.Json.Serialization;
using Goomer_Lista_Rango.Data;

namespace Goomer_Lista_Rango.DTOs;

public class ReadRestaurantDTO
{
    public int RestaurantId { get; set;  }
    public string Name { get; set; }
    public string? RestaurantPhoto { get; set; } = null;
    public Address Address { get; set; }
    public IEnumerable<Product> Product { get; set; }
    public IEnumerable<OpenHour> OpenHour { get; set; }
}