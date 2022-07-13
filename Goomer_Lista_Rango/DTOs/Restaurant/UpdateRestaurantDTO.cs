using System.ComponentModel.DataAnnotations;
using Goomer_Lista_Rango.Data;

namespace Goomer_Lista_Rango.DTOs;

public class UpdateRestaurantDTO
{
    [Required(ErrorMessage = $"Please provide a {nameof(Name)} for Restaurant")]
    [StringLength(30, MinimumLength = 5)]
    public string Name { get; set; }
    public IFormFile RestaurantPhoto { get; set; }
    
    [Required(ErrorMessage = $"Please provide a {nameof(AddressId)} for Restaurant")]
    public int AddressId { get; set; }
}

