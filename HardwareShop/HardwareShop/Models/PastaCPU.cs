using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{
    public class PastaCPU
    {
        public int Id { get; set; }

        [ForeignKey("ProdusId")]
        public Produs Produs { get; set; }
        public int ProdusId { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Conductivitate termica")]
        public string ConductivitateTermica { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Rezistenta termica")]
        public string RezistentaTermica { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Cantitate")]
        public int Cantitate { get; set; }
    }
}