using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareShop.Models
{
    public class BrandUser
    {
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string NumeBrand { get; set; }
    }
}