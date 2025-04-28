using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{
    public class GPU
    {
        public int Id { get; set; }

        [ForeignKey("ProdusId")]
        public Produs Produs { get; set; }
        public int ProdusId { get; set; }
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