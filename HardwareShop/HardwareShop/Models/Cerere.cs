using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{
    public class Cerere
    {
        public int Id { get; set; }

        [ForeignKey("UtilizatorId")]
        public ApplicationUser Utilizator { get; set; }

        [StringLength(128)]
        public string UtilizatorId {  get; set; }
        [Required(ErrorMessage = "Intoduceti CUI-ul firmei")]
        [Display(Name = "CUI Firma")]
        public string CUI { get; set; }
        [Required(ErrorMessage = "Intoduceti numele brand-ului")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Intoduceti numele orasului")]
        public string Oras { get; set; }

        public string Status { get; set; }

    }
}