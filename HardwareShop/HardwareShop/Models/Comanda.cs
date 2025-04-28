using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{
    public class Comanda
    {
        public int Id { get; set; }

        [ForeignKey("ClientId")]
        public ApplicationUser Client { get; set; }
        public string ClientId { get; set; }

        [ForeignKey("AdresaId")]
        public AdresaLivrare Adresa { get; set; }
        public int AdresaId { get; set; }

        [ForeignKey("FacturaId")]
        public Factura Factura { get; set; }
        public int FacturaId { get; set; }
        public float PretComanda { get; set; }
        

    }
}