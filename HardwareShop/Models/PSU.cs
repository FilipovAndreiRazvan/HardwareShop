using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{
    public class PSU
    {
        public int Id { get; set; }

        [ForeignKey("ProdusId")]
        public Produs Produs { get; set; }
        public int ProdusId { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        public int Putere { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Numar ventilatoare")]
        public int NrVentilatoare { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string Alimentare { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string Format { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Greutate(Kg)")]
        public float Greutate { get; set; }
    }
}