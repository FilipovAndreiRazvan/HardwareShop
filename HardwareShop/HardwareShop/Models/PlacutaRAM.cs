using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{
    public class PlacutaRAM
    {
        public int Id { get; set; }

        [ForeignKey("ProdusId")]
        public Produs Produs { get; set; }
        public int ProdusId { get; set; }
        [Display(Name = "Tip")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string Tip { get; set; }
        [Display(Name = "Capacitate")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public int Capacitate { get; set; }
        [Display(Name = "Frecventa")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public int Frecventa { get; set; }
        [Display(Name = "Module")]
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string Module { get; set; }
    }
}