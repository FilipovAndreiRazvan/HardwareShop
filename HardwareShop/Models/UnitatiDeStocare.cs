using System.ComponentModel.DataAnnotations;

namespace HardwareShop.Models
{
    public class UnitatiDeStocare : Produs
    {
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Form factor")]
        public string formFactor { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Capacitate")]
        public int capacitate { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Tip memorie")]
        public string tipMemorie { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Rata transfer citire")]
        public int rataTransferCitire { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Rata transfer scriere")]
        public int rataTransferScriere { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Dimensiune(cm)")]
        [RegularExpression(@"\d{2,3}x\d{2,3}x\d{2,3}", ErrorMessage = "Introduceti dimensiunea dupa model")]
        public string dimensiune { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Greutate(Kg)")]
        public float greutate { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Line Up")]
        public string lineUp { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Tip Controller")]
        public string tipController { get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Interfata")]
        public string interfata { get; set; }
    }
}