using System.ComponentModel.DataAnnotations;

namespace HardwareShop.Models
{
    public class GPU : Produs
    {
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Capacitate memorie")]
        public int CapacitateMemorie { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Frecventa memorie")]
        public int FrecventaMemorie { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Rezolutie maxima")]
        public string RezolutieMaxima { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Sistem de racire")]
        public string SistemRacire { get; set; }
    }
}