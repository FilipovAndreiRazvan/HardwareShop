using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{
    public class CPU
    {
        public int Id { get; set; }

        [ForeignKey("ProdusId")]
        public Produs Produs { get; set; }
        public int ProdusId { get; set; }
        [Required(ErrorMessage = "Camp Obligatoriu")]
        [Display(Name = "Frecventa de baza")]
        public float FrecventaBaza { get; set; }
        [Required(ErrorMessage = "Camp Obligatoriu")]
        [Display(Name = "Frecventa turbo")]
        public float FrecventaTurbo { get; set; }
        [Required(ErrorMessage = "Camp Obligatoriu")]
        [Display(Name = "Numar nuclee")]
        public int NrNuclee { get; set; }
        [Required(ErrorMessage = "Camp Obligatoriu")]
        [Display(Name = "Numar thread-uri")]
        public int NrThreads { get; set; }
        [Required(ErrorMessage = "Camp Obligatoriu")]
        [Display(Name = "Putere termica")]
        public string PutereTermica { get; set; }
        [Required(ErrorMessage = "Camp Obligatoriu")]
        [Display(Name = "Socket")]
        public string Socket { get; set; }
    }
}