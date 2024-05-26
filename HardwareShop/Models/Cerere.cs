using System.ComponentModel.DataAnnotations;

namespace HardwareShop.Models
{
    public class Cerere
    {
        public int Id { get; set; }
        public ApplicationUser Utilizator { get; set; }
        [Required(ErrorMessage = "Intoduceti CUI-ul firmei")]
        [Display(Name = "CUI Firma")]
        public string CUI { get; set; }
        [Required(ErrorMessage = "Intoduceti numele brand-ului")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Intoduceti numele orasului")]
        public string Oras { get; set; }
        [Display(Name = "Carcase")]
        public bool Carcase { get; set; }

        [Display(Name = "Placi de baza")]
        public bool placiDeBaza { get; set; }

        [Display(Name = "Procesoare")]
        public bool procesoare { get; set; }

        [Display(Name = "Placi video")]
        public bool placiVideo { get; set; }
        [Display(Name = "Placute RAM")]
        public bool placuteRAM { get; set; }

        [Display(Name = "Unitati de stocare")]
        public bool unitatiDeStocare { get; set; }
        [Display(Name = "Surse de alimentare")]
        public bool surse { get; set; }

        [Display(Name = "Pasta procesor")]
        public bool pastaProcesor { get; set; }

        public string Status { get; set; }
        public string tipCerere { get; set; }

        public Cerere()
        {
            Utilizator = new ApplicationUser();
        }
    }
}