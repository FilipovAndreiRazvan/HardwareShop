namespace HardwareShop.Models
{
    public class AdaugaProdusViewModel
    {
        public int Id { get; set; }
        public Produs Produs { get; set; }
        public Carcasa Carcasa { get; set; }
        public GPU PlacaVideo { get; set; }
        public PlacutaRAM PlacutaRAM { get; set; }
        public CPU Procesor { get; set; }
        public PSU Sursa { get; set; }
        public PastaCPU Pasta { get; set; }
        public UnitatiDeStocare Stocare { get; set; }

        public Motherboard PlacaDeBaza { get; set; }
        public Cerere cerere { get; set; }
    }
}