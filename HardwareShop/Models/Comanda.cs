using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{
    public class Comanda
    {
        public int Id { get; set; }
        public ApplicationUser client { get; set; }
        public AdresaLivrare adresa { get; set; }
        public Factura factura { get; set; }
        public float pretComanda { get; set; }
        [NotMapped]
        public string FacturaJson { get; set; }
        [NotMapped]
        public string AdresaJson { get; set; }
        [NotMapped]
        public string pretComandaJson { get; set; }

    }
}