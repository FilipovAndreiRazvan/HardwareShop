using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{
    public class Motherboard
    {
        public int Id { get; set; }

        [ForeignKey("ProdusId")]
        public Produs Produs { get; set; }
        public int ProdusId { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        public string Socket { get; set; }
        [Display(Name = "Chipset")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string Chipset { get; set; }
        [Display(Name = "Nr sloturi RAM")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public int NrSloturiRAM { get; set; }
        [Display(Name = "Tip RAM")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string TipRAM { get; set; }
        [Display(Name = "Sloturi PCIe")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string SloturiPCIe { get; set; }
        [Display(Name = "Conectori SATA")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string ConectoriSATA { get; set; }
        [Display(Name = "Conectori M2")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string ConectoriM2 { get; set; }
        [Display(Name = "Tip port USB")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string TipPortUSB { get; set; }
        [Display(Name = "Nr porturi USB")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public int NrPorturiUSB { get; set; }
        [Display(Name = "Form factor")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string FormFactor { get; set; }
    }
}