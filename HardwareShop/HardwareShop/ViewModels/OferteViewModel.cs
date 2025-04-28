using HardwareShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HardwareShop.ViewModels
{
    public class OferteViewModel<T>
    {
        public Produs Produs { get; set; }
        public T Categorie { get; set; }
    }
}