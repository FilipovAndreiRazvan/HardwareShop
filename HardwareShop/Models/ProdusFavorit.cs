using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{
    [Table("ProduseFavorites")]
    public class ProdusFavorit
    {
        public int Id { get; set; }
        public string categorie { get; set; }
        public int idProdus { get; set; }
        public ApplicationUser Utilizator { get; set; }

    }
}