using AutoMapper;
using HardwareShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HardwareShop.Service
{
    public class GestionareModeleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GestionareModeleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Adaugam noul produs in baza de date
        public async Task AdaugareModel(object produsSpecific)
        {
            
            switch (produsSpecific)
            {
                case Motherboard:
                    _context.placiDeBaza.Add((Motherboard)produsSpecific);
                    break;
                case Carcasa:
                    _context.carcase.Add((Carcasa)produsSpecific);
                    break;
                case GPU:
                    _context.placiVideo.Add((GPU)produsSpecific);
                    break;
                case PlacutaRAM:
                    _context.placuteRAM.Add((PlacutaRAM)produsSpecific);
                    break;
                case CPU:
                    _context.procesoare.Add((CPU)produsSpecific);
                    break;
                case PSU:
                    _context.surse.Add((PSU)produsSpecific);
                    break;
                case UnitatiDeStocare:
                    _context.stocare.Add((UnitatiDeStocare)produsSpecific);
                    break;
                case PastaCPU:
                    _context.pasteProcesor.Add((PastaCPU)produsSpecific);
                    break;
                default: throw new HttpException(404, "Categorie inexistenta!");
                
            }
            await _context.SaveChangesAsync();
        }
        public async Task<AdaugaProdusViewModel> CompletareModel( string categorie, int? idProdus)
        {
            AdaugaProdusViewModel model = new AdaugaProdusViewModel();
            Produs produs = new Produs();
            if (idProdus.HasValue)
            {
                produs = await _context.produse.FirstOrDefaultAsync(p => p.IdProdus == idProdus);
            }
            model.Produs = produs;
            switch (categorie)
            {
                case "Carcase":
                    model.Carcasa = idProdus.HasValue ? await _context.carcase.Include(c => c.Produs).SingleOrDefaultAsync(c => c.ProdusId == produs.IdProdus) : new Carcasa();
                    model.Produs.CategorieId = 1;
                    break;

                case "PlaciDeBaza":
                    model.PlacaDeBaza = idProdus.HasValue ? await _context.placiDeBaza.Include(c => c.Produs).SingleOrDefaultAsync(c => c.ProdusId == produs.IdProdus) : new Motherboard();
                    model.Produs.CategorieId = 2;
                    break;
                case "PlaciVideo":
                    model.PlacaVideo = idProdus.HasValue ? await _context.placiVideo.Include(c => c.Produs).SingleOrDefaultAsync(c => c.ProdusId == produs.IdProdus) : new GPU();
                    model.Produs.CategorieId = 3;
                    break;
                case "PlacuteRAM":
                    model.PlacutaRAM = idProdus.HasValue ? await _context.placuteRAM.Include(c => c.Produs).SingleOrDefaultAsync(c => c.ProdusId == produs.IdProdus) : new PlacutaRAM();
                    model.Produs.CategorieId = 4;
                    break;
                case "Procesoare":
                    model.Procesor = idProdus.HasValue ? await _context.procesoare.Include(c => c.Produs).SingleOrDefaultAsync(c => c.ProdusId == produs.IdProdus) : new CPU();
                    model.Produs.CategorieId = 5;
                    break;
                case "SurseDeAlimentare":
                    model.Sursa = idProdus.HasValue ? await _context.surse.Include(c => c.Produs).SingleOrDefaultAsync(c => c.ProdusId == produs.IdProdus) : new PSU();
                    model.Produs.CategorieId = 6;
                    break;
                case "UnitatiStocare":
                    model.Stocare = idProdus.HasValue ? await _context.stocare.Include(c => c.Produs).SingleOrDefaultAsync(c => c.ProdusId == produs.IdProdus) : new UnitatiDeStocare();
                    model.Produs.CategorieId = 7;
                    break;
                case "PastaProcesor":
                    model.Pasta = idProdus.HasValue ? await _context.pasteProcesor.Include(c => c.Produs).SingleOrDefaultAsync(c => c.ProdusId == produs.IdProdus) : new PastaCPU();
                    model.Produs.CategorieId = 8;
                    break;
                default:
                    throw new HttpException(404, "Categorie inexistenta!");

            }
            return model;

        }

        public async Task ActualizareModele(object produsSpecific, AdaugaProdusViewModel model)
        {
            // Actualizăm informațiile produsului pe baza celor din ViewModel
            var produs = await _context.produse.SingleOrDefaultAsync(p => p.IdProdus == model.Produs.IdProdus);
            _mapper.Map(model.Produs,produs);

            switch (produsSpecific)
            {
                case Motherboard:
                    model.PlacaDeBaza.Produs = produs;
                    model.PlacaDeBaza.ProdusId = produs.IdProdus;
                    var placa = await _context.placiDeBaza.SingleOrDefaultAsync(p => p.Id == model.PlacaDeBaza.Id);
                    _mapper.Map(model.PlacaDeBaza, placa);
                    break;

                case Carcasa:
                    model.Carcasa.Produs = produs;
                    model.Carcasa.ProdusId = produs.IdProdus;
                    var carcasa = await _context.carcase.SingleOrDefaultAsync(c => c.Id == model.Carcasa.Id);
                    _mapper.Map(model.Carcasa, carcasa);
                    break;
                
                case CPU:
                    model.Procesor.Produs = produs;
                    model.Procesor.ProdusId = produs.IdProdus;
                    var procesor = await _context.procesoare.SingleOrDefaultAsync(p => p.Id == model.Procesor.Id);
                    _mapper.Map(model.Procesor,procesor);
                    break;
                
                case GPU:
                    model.PlacaVideo.Produs = produs;
                    model.PlacaVideo.ProdusId = produs.IdProdus;
                    var placaVideo = await _context.placiVideo.SingleOrDefaultAsync(p => p.Id == model.PlacaVideo.Id);
                    _mapper.Map(model.PlacaVideo,placaVideo);
                    break;
                
                case PastaCPU:
                    model.Pasta.Produs = produs;
                    model.Pasta.ProdusId = produs.IdProdus;
                    var pastaCPU = await _context.pasteProcesor.SingleOrDefaultAsync(p => p.Id == model.Pasta.Id);
                    _mapper.Map(model.Pasta,pastaCPU);
                    break;

                case PlacutaRAM:
                    model.PlacutaRAM.Produs = produs;
                    model.PlacutaRAM.ProdusId = produs.IdProdus;
                    var placutaRAM = await _context.placuteRAM.SingleOrDefaultAsync(p => p.Id == model.PlacutaRAM.Id);
                    _mapper.Map(model.PlacutaRAM, placutaRAM);
                    break;

                case PSU:
                    model.Sursa.Produs = produs;
                    model.Sursa.ProdusId = produs.IdProdus;
                    var sursa = await _context.surse.SingleOrDefaultAsync(s => s.Id == model.Sursa.Id);
                    _mapper.Map(model.Sursa, sursa);
                    break;

                case UnitatiDeStocare:
                    model.Stocare.Produs = produs;
                    model.Stocare.ProdusId = produs.IdProdus;
                    var stocare = await _context.stocare.SingleOrDefaultAsync(s => s.Id == model.Stocare.Id);
                    _mapper.Map(model.Stocare, stocare);
                    break;
            }
            await _context.SaveChangesAsync();
        }
    }
}