using System.ComponentModel.DataAnnotations;

namespace HardwareShop.Models
{
    public class PastaCPU : Produs
    {
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Conductivitate termica")]
        public string conductivitateTermica { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Rezistenta termica")]
        public string rezistentaTermica { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Cantitate")]
        public int cantitate { get; set; }
    }
}