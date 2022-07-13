using System.Text.Json.Serialization;
using Goomer_Lista_Rango.Data;

namespace Goomer_Lista_Rango.DTOs;

public class ReadProductDTO
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
    public string? ProductPhoto { get; set; } = null;
    public int RestaurantId { get; set; }
}