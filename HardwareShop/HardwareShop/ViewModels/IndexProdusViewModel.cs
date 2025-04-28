using HardwareShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HardwareShop.ViewModels
{
    public class IndexProdusViewModel
    {
        public List<Produs> Produse {  get; set; }
        public List<Carcasa> Carcase { get; set; }
        public List<PastaCPU> PastaProcesor { get; set; }
        public List<Motherboard> PlaciDeBaza { get; set; }
        public List<GPU> PlaciVideo { get; set; }
        public List<PlacutaRAM> PlacuteRAM { get; set; }
        public List<CPU> Procesoare { get; set; }
        public List<PSU> SurseDeAlimentare { get; set; }
        public List<UnitatiDeStocare> UnitatiDeStocare { get; set; }
        public string categorieProduse { get; set; }
        public StatusProdus StatusProdus { get; set; }

    }
}