using System.ComponentModel.DataAnnotations;

namespace Goomer_Lista_Rango.DTOs;

public class UpdateDiscountDTO
{
    [Required(ErrorMessage = "It's necessary to provide a Discount Price")]
    [Range(5, 100, ErrorMessage = "It's only possible to set a value between 5.00$ and 100.00$")]
    public double DiscountPrice { get; set; }
}