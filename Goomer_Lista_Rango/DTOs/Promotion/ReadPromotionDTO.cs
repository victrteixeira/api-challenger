using Goomer_Lista_Rango.Data;

namespace Goomer_Lista_Rango.DTOs.Promotion;

public class ReadPromotionDTO
{
    public Discount Discount { get; set; }
    public DateTime PromotionBegins { get; set; }
    public DateTime PromotionEnds { get; set; }
    public string PromotionDescription { get; set; }
}