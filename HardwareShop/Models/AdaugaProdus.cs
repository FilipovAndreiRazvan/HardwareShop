namespace HardwareShop.Models
{
    public class AdaugaProdus
    {
        public int Id { get; set; }
        public bool listaFavorite { get; set; }
        public bool cosCumparaturi { get; set; }
        public Cerere cerere { get; set; }
    }
}