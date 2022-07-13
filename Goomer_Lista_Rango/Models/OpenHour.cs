using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Goomer_Lista_Rango.TimeAndDateOnlyConverter;

namespace Goomer_Lista_Rango.Data;
public class OpenHour
{
    [JsonIgnore]
    public int OpenHourId { get; internal set; }
    public string Day { get; internal set; }
    [property: JsonConverter(typeof(TimeOnlyConverter))]
    public TimeOnly OpenTime { get; internal set; }
    [property: JsonConverter(typeof(TimeOnlyConverter))]
    public TimeOnly CloseTime { get; internal set; }
    
    [JsonIgnore]
    public int RestaurantId { get; internal set; }
    [JsonIgnore]
    public Restaurant Restaurant { get; internal set; }
}