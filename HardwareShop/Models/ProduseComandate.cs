namespace HardwareShop.Models
{
    public class ProduseComandate
    {
        public int Id { get; set; }
        public Comanda IdComanda { get; set; }
        public int IdProdus { get; set; }
        public string Categorie { get; set; }
        public int Cantitate { get; set; }
    }
}