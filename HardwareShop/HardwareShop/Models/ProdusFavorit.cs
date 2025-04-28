using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{
    public class ProdusFavorit
    {
        public int Id { get; set; }

        [ForeignKey("ProdusId")]
        public Produs Produs { get; set; }
        public int ProdusId { get; set; }

        [ForeignKey("UtilizatorId")]
        public ApplicationUser Utilizator { get; set; }
        public string UtilizatorId { get; set; }

    }
}