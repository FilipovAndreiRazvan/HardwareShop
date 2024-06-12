using HardwareShop.Models;

namespace HardwareShop.ViewModels
{
    public class DetaliiComandaViewModel
    {
        public int Id { get; set; }
        public Comanda comanda { get; set; }
        public string etapa { get; set; }
        public Card card { get; set; }
    }
}