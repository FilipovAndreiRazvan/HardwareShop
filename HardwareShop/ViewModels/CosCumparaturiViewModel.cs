using HardwareShop.Models;
using System.Collections.Generic;

namespace HardwareShop.ViewModels
{
    public class CosCumparaturiViewModel
    {
        public int Id { get; set; }
        public List<Dictionary<int, Carcasa>> carcase { get; set; }
        public List<Dictionary<int, Motherboard>> motherboard { get; set; }
        public List<Dictionary<int, GPU>> gpu { get; set; }
        public List<Dictionary<int, CPU>> cpu { get; set; }
        public List<Dictionary<int, PlacutaRAM>> placuteRAM { get; set; }
        public List<Dictionary<int, UnitatiDeStocare>> unitatiDeStocare { get; set; }
        public List<Dictionary<int, PSU>> psu { get; set; }
        public List<Dictionary<int, PastaCPU>> pastaCPU { get; set; }


        public CosCumparaturiViewModel()
        {
            carcase = new List<Dictionary<int, Carcasa>>();
            gpu = new List<Dictionary<int, GPU>>();
            cpu = new List<Dictionary<int, CPU>>();
            motherboard = new List<Dictionary<int, Motherboard>>();
            placuteRAM = new List<Dictionary<int, PlacutaRAM>>();
            unitatiDeStocare = new List<Dictionary<int, UnitatiDeStocare>>();
            psu = new List<Dictionary<int, PSU>>();
            pastaCPU = new List<Dictionary<int, PastaCPU>>();
        }
    }
}