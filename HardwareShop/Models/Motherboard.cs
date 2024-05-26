using System.ComponentModel.DataAnnotations;

namespace HardwareShop.Models
{
    public class Motherboard : Produs
    {
        [Display(Name = "Socket")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string socket { get; set; }
        [Display(Name = "Chipset")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string chipset { get; set; }
        [Display(Name = "Nr sloturi RAM")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public int nrSloturiRAM { get; set; }
        [Display(Name = "Tip RAM")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string tipRAM { get; set; }
        [Display(Name = "Sloturi PCIe")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string sloturiPCIe { get; set; }
        [Display(Name = "Conectori SATA")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string conectoriSATA { get; set; }
        [Display(Name = "Conectori M2")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string conectoriM2 { get; set; }
        [Display(Name = "Tip port USB")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string tipPortUSB { get; set; }
        [Display(Name = "Nr porturi USB")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public int nrPorturiUSB { get; set; }
        [Display(Name = "Form factor")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string formFactor { get; set; }
    }
}