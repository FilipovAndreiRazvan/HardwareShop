namespace HardwareShop.Models
{
    public class Comanda
    {
        public int Id { get; set; }
        public string client { get; set; }
        public string CategorieProduse { get; set; }
        public string IndexProduse { get; set; }
        public AdresaLivrare adresa { get; set; }
        public Card card { get; set; }
    }
}