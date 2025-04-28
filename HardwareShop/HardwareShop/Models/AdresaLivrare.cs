using System.ComponentModel.DataAnnotations;

public class AdresaLivrare
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Camp obligatoriu")]
    [Display(Name = "Oras*")]
    public string Oras { get; set; }

    [Required(ErrorMessage = "Camp obligatoriu")]
    [Display(Name = "Localitate*")]
    public string Localitate { get; set; }

    [Required(ErrorMessage = "Camp obligatoriu")]
    [Display(Name = "Strada*")]
    public string Strada { get; set; }

    [Display(Name = "Bloc")]
    public string Bloc { get; set; }

    [Required(ErrorMessage = "Camp obligatoriu")]
    [Display(Name = "Numar*")]
    public string Numar { get; set; }
}