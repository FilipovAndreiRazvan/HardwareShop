using HardwareShop.Models;
using System.Collections.Generic;

namespace HardwareShop.ViewModels
{
    public class CosCumparaturiViewModel
    {
        public int Id { get; set; }
        List<Dictionary<int,Produs>> Produse {  get; set; }
    }
}