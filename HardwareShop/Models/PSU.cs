using System.ComponentModel.DataAnnotations;

namespace HardwareShop.Models
{
    public class PSU : Produs
    {
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Putere")]
        public int putere { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Numar ventilatoare")]
        public int nrVentilatoare { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Alimentare")]
        public string alimentare { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Format")]
        public string format { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Greutate(Kg)")]
        public float greutate { get; set; }
    }
}