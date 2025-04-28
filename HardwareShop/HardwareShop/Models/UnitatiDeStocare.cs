using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{
    public class UnitatiDeStocare
    {
        public int Id { get; set; }

        [ForeignKey("ProdusId")]
        public Produs Produs { get; set; }
        public int ProdusId { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Form factor")]
        public string FormFactor { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Capacitate")]
        public int Capacitate { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Tip memorie")]
        public string TipMemorie { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Rata transfer citire")]
        public int RataTransferCitire { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Rata transfer scriere")]
        public int RataTransferScriere { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Dimensiune(cm)")]
        [RegularExpression(@"\d{2,3}x\d{2,3}x\d{2,3}", ErrorMessage = "Introduceti dimensiunea dupa model")]
        public string Dimensiune { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Greutate(Kg)")]
        public float Greutate { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Line Up")]
        public string LineUp { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Tip Controller")]
        public string TipController { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Interfata")]
        public string Interfata { get; set; }
    }
}