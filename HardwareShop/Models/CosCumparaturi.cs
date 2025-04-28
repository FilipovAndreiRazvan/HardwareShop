using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{

    public class CosCumparaturi
    {
        public int Id { get; set; }
        [ForeignKey("ClientId")]
        public ApplicationUser Client { get; set; }
        public string ClientId { get; set; }

        [ForeignKey("ProdusId")]
        public Produs Produs { get; set; }
        public int ProdusId { get; set; }

        public int NrBuc { get; set; }
    }
}