using System.ComponentModel.DataAnnotations;

namespace HardwareShop.Models
{
    public class PlacutaRAM : Produs
    {
        [Display(Name = "Tip")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string tip { get; set; }
        [Display(Name = "Capacitate")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public int capacitate { get; set; }
        [Display(Name = "Frecventa")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public int frecventa { get; set; }
        [Display(Name = "Module")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string module { get; set; }
    }
}