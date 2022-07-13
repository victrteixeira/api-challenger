using System.ComponentModel.DataAnnotations;
using Goomer_Lista_Rango.Data;

namespace Goomer_Lista_Rango.DTOs.Promotion;

public class CreatePromotionDTO
{
    private const string _bannedChar = "'*\"()[];@!";
    
    [Required(ErrorMessage = "Please provide an Id of Discount")]
    public int DiscountId { get; set; }
    
    [Required(ErrorMessage = "Please provide an Id of Product")]
    public int ProductId { get; set; }

    private string _promotionDescription;

    public string PromotionDescription
    {
        get => _promotionDescription;
        set
        {
            if (value.Any(ch => _bannedChar.Contains(ch)))
                throw new BannedCharException();

            _promotionDescription = value;
        }
    }

    [Required(ErrorMessage = "Please set when Promotion start")]
    public string PromotionBegins
    {
        get => _promotionBegins;
        set
        {
            if (value.Any(ch => _bannedChar.Contains(ch)))
                throw new BannedCharException();

            _promotionBegins = value;
        }
    }

    private string _promotionBegins;

    [Required(ErrorMessage = "Please set when Promotion end")]
    public string PromotionEnds
    {
        get => _promotionEnds;
        set
        {
            if (value.Any(ch => _bannedChar.Contains(ch)))
                throw new BannedCharException();

            _promotionEnds = value;
        }
    }

    private string _promotionEnds;
}