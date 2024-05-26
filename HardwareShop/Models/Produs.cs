using System.ComponentModel.DataAnnotations;

namespace HardwareShop.Models
{
    public class Produs
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        public string Nume { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        public float Pret { get; set; }
        [Display(Name = "Descriere (optional)")]
        public string Descriere { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Incarca o imagine")]
        public string imgLink { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Stoc")]
        public int stoc { get; set; }
        public string Brand { get; set; }
        public bool produsFavorit { get; set; }
        public string categorie { get; set; }
        public void cumparat()
        {
            this.stoc--;
        }

    }
}