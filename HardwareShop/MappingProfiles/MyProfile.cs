using AutoMapper;
using HardwareShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HardwareShop.MappingProfiles
{
    public class MyProfile : Profile
    {
        public MyProfile()
        {
            CreateMap<Produs, Produs>();
            CreateMap<Carcasa, Carcasa>();
            CreateMap<Motherboard, Motherboard>();
            CreateMap<CPU, CPU>();
            CreateMap<GPU, GPU>();
            CreateMap<PastaCPU, PastaCPU>();
            CreateMap<PlacutaRAM, PlacutaRAM>();
            CreateMap<PSU, PSU>();
            CreateMap<UnitatiDeStocare, UnitatiDeStocare>();
            
        }
    }
}