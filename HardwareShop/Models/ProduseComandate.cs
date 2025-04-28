using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{
    public class ProdusComandat
    {
        public int Id { get; set; }

        [ForeignKey("ComandaId")]
        public Comanda Comanda { get; set; }
        public int ComandaId { get; set; }

        [ForeignKey("ProdusId")]
        public Produs Produs { get; set; }
        public int ProdusId { get; set; }

        [ForeignKey("CategorieId")]
        public Categorie Categorie { get; set; }
        public byte CategorieId { get; set; }
        public int Cantitate { get; set; }
    }
}