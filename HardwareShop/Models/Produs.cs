using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{
    public class Produs
    {
        [Key]
        public int IdProdus { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]

        [MaxLength(40,ErrorMessage = "Lungimea maxima este de 40 de caractere")]
        public string Nume { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        public decimal Pret { get; set; }
        [Display(Name = "Descriere (optional)")]
        public string Descriere { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Incarca o imagine")]
        public string ImgLink { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Stoc")]
        public int Stoc { get; set; }
        public BrandUser Brand { get; set; }
        public int BrandId { get; set; }
        public Categorie Categorie { get; set; }
        public byte CategorieId { get; set; }
        public decimal? Oferta { get; set; }

        [NotMapped]
        public bool ProdusFavorit { get; set; }

    }
}