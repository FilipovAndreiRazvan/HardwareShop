namespace HardwareShop.Models
{
    public class BrandUser
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public string numeBrand { get; set; }
    }
}