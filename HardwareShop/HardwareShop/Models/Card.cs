using System.ComponentModel.DataAnnotations;

namespace HardwareShop.Models
{
    public class Card
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Camp obligstoriu")]
        [Display(Name = "Nume detinator")]
        public string NumeDetinator { get; set; }
        [Required(ErrorMessage = "Camp obligstoriu")]
        [Display(Name = "Numar card")]
        public string NrCard { get; set; }
        [Required(ErrorMessage = "Camp obligstoriu")]
        public int CVC { get; set; }
        public float Sold { get; set; }
    }
}