using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{
    public class Carcasa
    {
        public int Id { get; set; }

        [ForeignKey("ProdusId")]
        public Produs Produs { get; set; }
        public int ProdusId {  get; set; }
        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Format")]
        public string Format { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Numar ventilatoare")]
        public int NrVentilatoare { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Deschidere laterala")]
        public bool DeschidereLaterala { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Tip")]
        public string TipCarcasa { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Numar locase sloturi extensii")]
        public int NrLocaseSloturiExtensii { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Dimensiune(cm)")]
        [RegularExpression(@"\d{2,3} x \d{2,3} x \d{2,3}", ErrorMessage = "Introduceti dimensiunea dupa model")]
        public string Dimensiune { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Greutate(Kg)")]
        public float Greutate { get; set; }

        [Required(ErrorMessage = "Camp obligatoriu")]
        [Display(Name = "Culoare")]
        public string Culoare { get; set; }
    }
}