using System.ComponentModel.DataAnnotations;

namespace HardwareShop.Models
{
    public class CPU : Produs
    {
        [Required(ErrorMessage = "Camp Obligatoriu")]
        [Display(Name = "Frecventa de baza")]
        public float frecventaBaza { get; set; }
        [Required(ErrorMessage = "Camp Obligatoriu")]
        [Display(Name = "Frecventa turbo")]
        public float frecventaTurbo { get; set; }
        [Required(ErrorMessage = "Camp Obligatoriu")]
        [Display(Name = "Numar nuclee")]
        public int nrNuclee { get; set; }
        [Required(ErrorMessage = "Camp Obligatoriu")]
        [Display(Name = "Numar thread-uri")]
        public int nrThreads { get; set; }
        [Required(ErrorMessage = "Camp Obligatoriu")]
        [Display(Name = "Putere termica")]
        public string putereTermica { get; set; }
        [Required(ErrorMessage = "Camp Obligatoriu")]
        [Display(Name = "Socket")]
        public string socket { get; set; }
    }
}