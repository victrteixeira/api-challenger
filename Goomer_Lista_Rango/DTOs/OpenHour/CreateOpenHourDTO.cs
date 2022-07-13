using System.ComponentModel.DataAnnotations;
using Goomer_Lista_Rango.Data;

namespace Goomer_Lista_Rango.DTOs;

public class CreateOpenHourDTO
{
    [Required(ErrorMessage = "It's necessary to set a day between Sunday and Saturday")]
    public string DayOfWeek { get; set; }
    [Required(ErrorMessage = "It's necessary to set a open time")]
    [StringLength(7, MinimumLength = 5, ErrorMessage = "It's no possible to add it. Send as aside: HH:mm")]
    public string OpeningTime { get; set; }
    [Required(ErrorMessage = "It's necessary to set a close time")]
    [StringLength(7, MinimumLength = 5, ErrorMessage = "It's no possible to add it. Send as aside: HH:mm")]
    public string ClosingTime { get; set; }
}