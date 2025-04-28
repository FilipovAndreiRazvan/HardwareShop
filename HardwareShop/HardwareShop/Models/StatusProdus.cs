using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HardwareShop.Models
{
    public class StatusProdus
    {
        public int Id { get; set; } 
        public string ProdusAdaugatCos {  get; set; }
        public string ProdusAdaugatListaFavorite {  get; set; }
        public string ProdusAdaugat {  get; set; }
        public string ProdusModificat {  get; set; }
        public string ProdusSters {  get; set; }
    }
}