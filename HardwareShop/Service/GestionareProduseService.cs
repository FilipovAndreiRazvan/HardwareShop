using HardwareShop.Models;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using HardwareShop.ViewModels;
using System.Collections.Generic;

namespace HardwareShop.Service
{

    public class GestionareProduseService
    {
        private readonly GestionareModeleService _gestionareModeleService;
        private ApplicationDbContext _context;

        public GestionareProduseService(ApplicationDbContext context,GestionareModeleService gestionareModeleService)
        {
            _gestionareModeleService = gestionareModeleService;
            _context = context;
        }
        public async  Task StergeProdus(ApplicationDbContext _context,int id)
        {
            var produs = await _context.produse.Include(p => p.Categorie).FirstOrDefaultAsync(p => p.IdProdus == id);

            if (produs == null)
            {
                throw new HttpException(404, "Produs inexistent!");
            }

            await StergereProdusCos(_context, produs);
            await StergereProdusFavorite(_context, produs);
            _context.produse.Remove(produs);

            await _context.SaveChangesAsync();

        }

        public async Task StergereProdusCos(ApplicationDbContext _context, Produs produs)
        {
            var cos = await _context.cos.Where(c => c.ProdusId == produs.IdProdus).ToListAsync();

            _context.cos.RemoveRange(cos);
        }
        public async Task StergereProdusFavorite(ApplicationDbContext _context, Produs produs)
        {
            var listaFavorite = await _context.produseFavorite.Where(c => c.ProdusId == produs.IdProdus).ToListAsync();

            _context.produseFavorite.RemoveRange(listaFavorite);
        }

        public async Task SalvareProdus(string userId, AdaugaProdusViewModel model)
        {
            var brand = await _context.branduri.SingleOrDefaultAsync(b => b.User.Id == userId);
            await SalvareProdus(model, brand);
        }
        public async Task<IndexProdusViewModel> ListareProduse(ApplicationDbContext _context,string userId, string categorie)
        {
            var brandEntity = await _context.branduri.SingleOrDefaultAsync(b => b.User.Id == userId);

            var brand = brandEntity?.NumeBrand;

            return (IndexProdusViewModel)await ListaProduse(userId, categorie, numeBrand: brand);
        }

        public void AdaugareLinkImagineProdus(ApplicationDbContext _context,object produs)
        {
            // Verificăm tipul produsului și actualizăm linkul imaginii corespunzător.
            if (produs is Carcasa carcasa)
            {
                carcasa.Produs.ImgLink = "Imagini/Carcase/" + carcasa.Produs.ImgLink;
                _context.carcase.Add(carcasa);
            }
            else if (produs is PastaCPU pastaCpu)
            {
                pastaCpu.Produs.ImgLink = "Imagini/PasteProcesor/" + pastaCpu.Produs.ImgLink;
                _context.pasteProcesor.Add(pastaCpu);
            }
            else if (produs is Motherboard placaBaza)
            {
                placaBaza.Produs.ImgLink = "Imagini/PlaciDeBaza/" + placaBaza.Produs.ImgLink;
                _context.placiDeBaza.Add(placaBaza);
            }
            else if (produs is GPU placaVideo)
            {
                placaVideo.Produs.ImgLink = "Imagini/PlaciVideo/" + placaVideo.Produs.ImgLink;
                _context.placiVideo.Add(placaVideo);
            }
            else if (produs is PlacutaRAM placuteRAM)
            {
                placuteRAM.Produs.ImgLink = "Imagini/PlacuteRAM/" + placuteRAM.Produs.ImgLink;
                _context.placuteRAM.Add(placuteRAM);
            }
            else if (produs is CPU procesor)
            {
                procesor.Produs.ImgLink = "Imagini/Procesoare/" + procesor.Produs.ImgLink;
                _context.procesoare.Add(procesor);
            }
            else if (produs is PSU sursa)
            {
                sursa.Produs.ImgLink = "Imagini/SursePC/" + sursa.Produs.ImgLink;
                _context.surse.Add(sursa);
            }
            else if (produs is UnitatiDeStocare stocare)
            {
                stocare.Produs.ImgLink = "Imagini/UnitatiDeStocare/" + stocare.Produs.ImgLink;
                _context.stocare.Add(stocare);
            }


        }


        // Această metodă returnează lista produselor dintr-o categorie, cu opțiunea de a filtra după brand sau id-ul produsului.
        public async Task<object> ListaProduse(string userId, string categorieProduse, string numeBrand = null, int produsId = 0)
        {
            AdaugaProdusViewModel model = new AdaugaProdusViewModel();
            // Folosim un switch pentru a gestiona diferite categorii de produse.
            switch (categorieProduse)
            {
                case "Carcase":
                    List<Carcasa> carcase;
                    if (produsId != 0)
                    {
                        model.Carcasa = await _context.carcase.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).SingleOrDefaultAsync(p => p.Produs.IdProdus == produsId);
                        model.Produs = model.Carcasa.Produs;
                        return model;
                    }
                     if (!string.IsNullOrEmpty(numeBrand))
                    {
                        carcase = await _context.carcase.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).Where(p => p.Produs.Brand.NumeBrand == numeBrand).ToListAsync();
                    }
                    else
                    {
                        carcase = await _context.carcase.Include(p => p.Produs).Include(p => p.Produs.Categorie).ToListAsync();
                    }
                    return VerificareProdus<Carcasa>(userId, carcase);

                case "PastaProcesor":
                    List<PastaCPU> pastaCPU;
                    if (produsId != 0)
                        return await _context.pasteProcesor.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).SingleOrDefaultAsync(p => p.Produs.IdProdus == produsId);
                    if (!string.IsNullOrEmpty(numeBrand))
                    {
                        pastaCPU = await _context.pasteProcesor.Include(p => p.Produs).Include(p => p.Produs.Categorie).Where(p => p.Produs.Brand.NumeBrand == numeBrand).ToListAsync();
                    }
                    pastaCPU = await _context.pasteProcesor.Include(p => p.Produs).Include(p => p.Produs.Categorie).ToListAsync();
                    return VerificareProdus<PastaCPU>(userId, pastaCPU);

                case "PlaciDeBaza":
                    List<Motherboard> placiDeBaza;
                    if (produsId != 0)
                        return _context.placiDeBaza.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).SingleOrDefault(p => p.Produs.IdProdus == produsId);
                    if (!string.IsNullOrEmpty(numeBrand))
                    {
                        placiDeBaza = await _context.placiDeBaza.Include(p => p.Produs).Include(p => p.Produs.Categorie).Where(p => p.Produs.Brand.NumeBrand == numeBrand).ToListAsync();
                    }
                    placiDeBaza = await _context.placiDeBaza.Include(p => p.Produs).Include(p => p.Produs.Categorie).ToListAsync();
                    return VerificareProdus<Motherboard>(userId, placiDeBaza);

                case "PlaciVideo":
                    List<GPU> placiVideo;
                    if (produsId != 0)
                        return _context.placiVideo.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).SingleOrDefault(p => p.Produs.IdProdus == produsId);
                    if (!string.IsNullOrEmpty(numeBrand))
                    {
                        placiVideo = await _context.placiVideo.Include(p => p.Produs).Include(p => p.Produs.Categorie).Where(p => p.Produs.Brand.NumeBrand == numeBrand).ToListAsync();
                    }
                    placiVideo = await _context.placiVideo.Include(p => p.Produs).Include(p => p.Produs.Categorie).ToListAsync();
                    return VerificareProdus<GPU>(userId, placiVideo);

                case "PlacuteRAM":
                    List<PlacutaRAM> placuteRAM;
                    if (produsId != 0)
                        return _context.placuteRAM.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).SingleOrDefault(p => p.Produs.IdProdus == produsId);
                    if (!string.IsNullOrEmpty(numeBrand))
                    {
                        placuteRAM = await _context.placuteRAM.Include(p => p.Produs).Include(p => p.Produs.Categorie).Where(p => p.Produs.Brand.NumeBrand == numeBrand).ToListAsync();
                    }
                    placuteRAM = await _context.placuteRAM.Include(p => p.Produs).Include(p => p.Produs.Categorie).ToListAsync();
                    return VerificareProdus<PlacutaRAM>(userId, placuteRAM);

                case "Procesoare":
                    List<CPU> procesoare;
                    if (produsId != 0)
                        return _context.procesoare.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).SingleOrDefault(p => p.Produs.IdProdus == produsId);
                    if (!string.IsNullOrEmpty(numeBrand))
                    {
                        procesoare = await _context.procesoare.Include(p => p.Produs).Include(p => p.Produs.Categorie).Where(p => p.Produs.Brand.NumeBrand == numeBrand).ToListAsync();
                    }
                    procesoare = await _context.procesoare.Include(p => p.Produs).Include(p => p.Produs.Categorie).ToListAsync();
                    return VerificareProdus<CPU>(userId, procesoare);

                case "SurseDeAlimentare":
                    List<PSU> surse;
                    if (produsId != 0)
                        return _context.surse.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).SingleOrDefault(p => p.Produs.IdProdus == produsId);
                    if (!string.IsNullOrEmpty(numeBrand))
                    {
                        surse = await _context.surse.Include(p => p.Produs).Include(p => p.Produs.Categorie).Where(p => p.Produs.Brand.NumeBrand == numeBrand).ToListAsync();
                    }
                    surse = await _context.surse.Include(p => p.Produs).Include(p => p.Produs.Categorie).ToListAsync();
                    return VerificareProdus(userId, surse);

                default:
                    List<UnitatiDeStocare> unitatiStocare;
                    if (produsId != 0)
                        return _context.stocare.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).SingleOrDefault(p => p.Produs.IdProdus == produsId);
                    if (!string.IsNullOrEmpty(numeBrand))
                    {
                        unitatiStocare = await _context.stocare.Include(p => p.Produs).Include(p => p.Produs.Categorie).Where(p => p.Produs.Brand.NumeBrand == numeBrand).ToListAsync();
                    }
                    unitatiStocare = await _context.stocare.Include(p => p.Produs).Include(p => p.Produs.Categorie).ToListAsync();
                    return VerificareProdus<UnitatiDeStocare>(userId, unitatiStocare);
            }

        }

        // Verifică produsele favoritate ale unui utilizator pentru fiecare categorie de produs
        public IndexProdusViewModel VerificareProdus<T>(string userId, List<T> categorii)
        {
            List<Produs> produse = new List<Produs>();
            foreach (var produs in categorii)
            {
                if (produs is Carcasa carcasa)
                {
                    produse.Add(carcasa.Produs);
                    carcasa.Produs.ProdusFavorit = _context.produseFavorite.
                        Any(p => p.ProdusId == carcasa.ProdusId &&
                        p.UtilizatorId == userId);
                }
                if (produs is PastaCPU pasta)
                {
                    produse.Add(pasta.Produs);
                    pasta.Produs.ProdusFavorit = _context.produseFavorite.
                        Any(p => p.ProdusId == pasta.ProdusId &&
                        p.UtilizatorId == userId);
                }
                if (produs is Motherboard placaDeBaza)
                {
                    produse.Add(placaDeBaza.Produs);
                    placaDeBaza.Produs.ProdusFavorit = _context.produseFavorite.
                       Any(p => p.ProdusId == placaDeBaza.ProdusId &&
                       p.UtilizatorId == userId);
                }
                if (produs is GPU placaVideo)
                {
                    produse.Add(placaVideo.Produs);
                    placaVideo.Produs.ProdusFavorit = _context.produseFavorite.
                       Any(p => p.ProdusId == placaVideo.ProdusId &&
                       p.UtilizatorId == userId);
                }
                if (produs is PlacutaRAM placutaRAM)
                {
                    produse.Add(placutaRAM.Produs);
                    placutaRAM.Produs.ProdusFavorit = _context.produseFavorite.
                       Any(p => p.ProdusId == placutaRAM.ProdusId &&
                       p.UtilizatorId == userId);
                }
                if (produs is CPU procesor)
                {
                    produse.Add(procesor.Produs);
                    procesor.Produs.ProdusFavorit = _context.produseFavorite.
                      Any(p => p.ProdusId == procesor.ProdusId &&
                      p.UtilizatorId == userId);
                }
                if (produs is PSU sursa)
                {
                    produse.Add(sursa.Produs);
                    sursa.Produs.ProdusFavorit = _context.produseFavorite.
                      Any(p => p.ProdusId == sursa.ProdusId &&
                      p.UtilizatorId == userId);
                }
                if (produs is UnitatiDeStocare unitate)
                {
                    produse.Add(unitate.Produs);
                    unitate.Produs.ProdusFavorit = _context.produseFavorite.
                      Any(p => p.ProdusId == unitate.ProdusId &&
                      p.UtilizatorId == userId);
                }

            }
            IndexProdusViewModel model = new IndexProdusViewModel();
            model.Produse = produse;
            if (categorii is List<Carcasa> listaCarcase)
            {
                model.Carcase = listaCarcase;
            }
            else if (categorii is List<PastaCPU> listaPastaCPU)
            {
                model.PastaProcesor = listaPastaCPU;
            }
            else if (categorii is List<Motherboard> listaPlaciDeBaza)
            {
                model.PlaciDeBaza = listaPlaciDeBaza;
            }
            else if (categorii is List<GPU> listaPlaciVideo)
            {
                model.PlaciVideo = listaPlaciVideo;
            }
            else if (categorii is List<PlacutaRAM> listaPlacuteRAM)
            {
                model.PlacuteRAM = listaPlacuteRAM;
            }
            else if (categorii is List<CPU> listaProcesoare)
            {
                model.Procesoare = listaProcesoare;
            }
            else if (categorii is List<PSU> listaSurseDeAlimantare)
            {
                model.SurseDeAlimentare = listaSurseDeAlimantare;
            }
            else if (categorii is List<UnitatiDeStocare> listaUnitatiDeStoacare)
            {
                model.UnitatiDeStocare = listaUnitatiDeStoacare;
            }

            return model;
        }


        // Asociază un produs cu o categorie pe baza datelor din ViewModel

        public async Task SalvareProdus(AdaugaProdusViewModel model, BrandUser brand)
        {
            var produs = model.Produs;
            if (model.PlacaDeBaza != null)
            {
                await SalvareProdusSpecific(model.PlacaDeBaza, brand, model);
            }
            else if (model.Carcasa != null)
            {
                model.Produs.ImgLink = "Imagini/Carcase/" + model.Produs.ImgLink;
                await SalvareProdusSpecific(model.Carcasa, brand, model);
            }
            else if (model.Procesor != null)
            {
                model.Produs.ImgLink = "Imagini/Procesoare/" + model.Produs.ImgLink;
                await SalvareProdusSpecific(model.Procesor, brand, model);
            }
            else if (model.Pasta != null)
            {
                model.Produs.ImgLink = "Imagini/PasteProcesor/" + model.Produs.ImgLink;
                await SalvareProdusSpecific(model.Pasta, brand, model);
            }
            else if (model.PlacaVideo != null)
            {
                model.Produs.ImgLink = "Imagini/PlaciVideo/" + model.Produs.ImgLink;
                await SalvareProdusSpecific(model.PlacaVideo, brand, model);
            }
            else if (model.PlacutaRAM != null)
            {
                model.Produs.ImgLink = "Imagini/PlacuteRAM/" + model.Produs.ImgLink;
                await SalvareProdusSpecific(model.PlacutaRAM, brand, model);
            }
            else if (model.Sursa != null)
            {
                model.Produs.ImgLink = "Imagini/Surse/" + model.Produs.ImgLink;
                await SalvareProdusSpecific(model.Sursa, brand, model);
            }
            else if (model.Stocare != null)
            {
                model.Produs.ImgLink = "Imagini/UnitatiDeStocare/" + model.Produs.ImgLink;
                await SalvareProdusSpecific(model.Stocare, brand, model);
            }
        }

        public async Task SalvareProdusSpecific(object produsSpecific, BrandUser brand, AdaugaProdusViewModel model)
        {
            if (model.Produs.IdProdus != 0)
            {
                model.Produs.BrandId = brand.Id;
                model.Produs.Brand = brand;
                await _gestionareModeleService.ActualizareModele(produsSpecific, model);
            }
            else
            {
                var product = produsSpecific as dynamic;
                product.Produs = model.Produs;
                product.Produs.BrandId = brand.Id;
                _context.produse.Add(model.Produs);
            }
        }

    }
}