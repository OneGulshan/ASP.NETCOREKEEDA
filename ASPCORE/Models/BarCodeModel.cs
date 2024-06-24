using System.ComponentModel.DataAnnotations;

namespace ASPCORE.Models
{
    [Display(Name = "Enter BarCode Text")]
    public class BarCodeModel
    {
        public string BarCodeText { get; set; }
    }
}
