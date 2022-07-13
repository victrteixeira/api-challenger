using Goomer_Lista_Rango.Data;

namespace Goomer_Lista_Rango.DTOs;

public class ReadOpenHourDTO
{
    public int OpenHourId { get; set; }
    public string DayOfWeek { get; set; }
    public TimeOnly OpeningTime { get; set; }
    public TimeOnly ClosingTime { get; set; }
    public Restaurant Restaurant { get; set; }
}