using System.ComponentModel.DataAnnotations;

namespace Goomer_Lista_Rango.DTOs;

public class UpdateProductDTO
{
    [Required(ErrorMessage = $"Please provide a {nameof(Name)} for Product")]
    [StringLength(30, MinimumLength = 3)]
    public string Name { get; set; }
    [Required(ErrorMessage = $"Please provide a {nameof(Category)}")]
    [StringLength(30, MinimumLength = 2)]
    public string Category { get; set; }
    [Required(ErrorMessage = $"Please provide a {nameof(Price)}")]
    public double Price { get; set; }
    public IFormFile ProductPhoto { get; set; }
    [Required(ErrorMessage = $"Please provide a {nameof(RestaurantId)}")]
    [StringLength(30, MinimumLength = 2)]
    public int RestaurantId { get; set; }
}