using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Factura
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Camp obligatoriu")]
    [Display(Name = "Nume*")]
    public string Nume { get; set; }

    [Required(ErrorMessage = "Camp obligatoriu")]
    [Display(Name = "Prenume*")]
    public string Prenume { get; set; }

    [Required(ErrorMessage = "Camp obligatoriu")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [Display(Name = "Email*")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Camp obligatoriu")]
    [Display(Name = "Telefon*")]
    public int NrTelefon { get; set; }

    [Required]
    [ForeignKey("AdresaId")]
    public AdresaLivrare Adresa { get; set; }
    public int AdresaId { get; set; }
}