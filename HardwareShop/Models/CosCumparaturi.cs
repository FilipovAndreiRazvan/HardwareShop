using System.Collections.Generic;

namespace HardwareShop.Models
{

    public class CosCumparaturi
    {
        public int Id { get; set; }
        public string categorie { get; set; }
        public int idProdus { get; set; }
        public List<Carcasa> carcase { get; set; }
        public List<Motherboard> motherboard { get; set; }
        public List<GPU> gpu { get; set; }
        public List<Produs> cpu { get; set; }
        public List<PlacutaRAM> placuteRAM { get; set; }
        public List<UnitatiDeStocare> unitatiDeStocare { get; set; }
        public List<PSU> psu { get; set; }
        public List<PastaCPU> pastaCPU { get; set; }


        public CosCumparaturi()
        {
            carcase = new List<Carcasa>();
            gpu = new List<GPU>();
            cpu = new List<Produs>();
            motherboard = new List<Motherboard>();
            placuteRAM = new List<PlacutaRAM>();
            unitatiDeStocare = new List<UnitatiDeStocare>();
            psu = new List<PSU>();
            pastaCPU = new List<PastaCPU>();
        }
        public string utilizator { get; set; }
    }
}