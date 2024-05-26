using System.ComponentModel.DataAnnotations;

namespace HardwareShop.Models
{
    public class Carcasa : Produs
    {
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Format")]
        public string format { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Numar ventilatoare")]
        public int nrVentilatoare { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Deschidere laterala")]
        public bool deschidereLaterala { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Tip")]
        public string tipCarcasa { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Numar locase sloturi extensii")]
        public int nrLocaseSloturiExtensii { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Dimensiune(cm)")]
        [RegularExpression(@"\d{2,3}x\d{2,3}x\d{2,3}", ErrorMessage = "Introduceti dimensiunea dupa model")]
        public string dimensiune { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Greutate(Kg)")]
        public float greutate { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Culoare")]
        public string culoare { get; set; }
    }
}