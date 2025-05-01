using HardwareShop.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using HardwareShop.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;

// Controller-ul gestionează metodele externe pentru adăugarea și manipularea produselor hardware.
namespace HardwareShop.Models
{
    public  class MetodeExterne
    {
        // Constructorul primește contextul bazei de date pentru a putea interacționa cu entitățile din aplicație
        private readonly ApplicationDbContext context;
        public MetodeExterne(ApplicationDbContext _context) 
        {
            context = _context;
        }

        // Adaugă un produs în funcție de tipul acestuia (Carcasa, PastaCPU, etc.)
        public void AdaugareProdus(object produs)
        {
            // Verificăm tipul produsului și actualizăm linkul imaginii corespunzător.
            if (produs is Carcasa carcasa)
            {
                carcasa.Produs.ImgLink = "Imagini/Carcase/" + carcasa.Produs.ImgLink;
                context.carcase.Add(carcasa);
            }
            else if (produs is PastaCPU pastaCpu)
            {
                pastaCpu.Produs.ImgLink = "Imagini/PasteProcesor/" + pastaCpu.Produs.ImgLink;
                context.pasteProcesor.Add(pastaCpu);
            }
            else if (produs is Motherboard placaBaza)
            {
                placaBaza.Produs.ImgLink = "Imagini/PlaciDeBaza/" + placaBaza.Produs.ImgLink;
                context.placiDeBaza.Add(placaBaza);
            }
            else if (produs is GPU placaVideo)
            {
                placaVideo.Produs.ImgLink = "Imagini/PlaciVideo/" + placaVideo.Produs.ImgLink;
                context.placiVideo.Add(placaVideo);
            }
            else if (produs is PlacutaRAM placuteRAM)
            {
                placuteRAM.Produs.ImgLink = "Imagini/PlacuteRAM/" + placuteRAM.Produs.ImgLink;
                context.placuteRAM.Add(placuteRAM);
            }
            else if (produs is CPU procesor)
            {
                procesor.Produs.ImgLink = "Imagini/Procesoare/" + procesor.Produs.ImgLink;
                context.procesoare.Add(procesor);
            }
            else if (produs is PSU sursa)
            {
                sursa.Produs.ImgLink = "Imagini/SursePC/" + sursa.Produs.ImgLink;
                context.surse.Add(sursa);
            }
            else if (produs is UnitatiDeStocare stocare)
            {
                stocare.Produs.ImgLink = "Imagini/UnitatiDeStocare/" + stocare.Produs.ImgLink;
                context.stocare.Add(stocare);
            }


        }

        // Această metodă returnează lista produselor dintr-o categorie, cu opțiunea de a filtra după brand sau id-ul produsului.
        public async Task<object> ListaProduse(string userId,string categorieProduse, string numeBrand = null, int produsId = 0)
        {
            // Folosim un switch pentru a gestiona diferite categorii de produse.
            switch (categorieProduse)
            {
                case "Carcase":
                    List<Carcasa> carcase;
                    if (produsId != 0)
                        return await context.carcase.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).SingleOrDefaultAsync(p => p.Produs.IdProdus == produsId);
                    if (!string.IsNullOrEmpty(numeBrand))
                    {
                        carcase = await context.carcase.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).Where(p => p.Produs.Brand.NumeBrand == numeBrand).ToListAsync();
                    }
                    else
                    {
                        carcase = await context.carcase.Include(p => p.Produs).Include(p => p.Produs.Categorie).ToListAsync();
                    }
                    return Verificare<Carcasa>(userId,carcase);

                case "PastaProcesor":
                    List<PastaCPU> pastaCPU;
                    if (produsId != 0)
                        return await context.pasteProcesor.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).SingleOrDefaultAsync(p => p.Produs.IdProdus == produsId);
                    if (!string.IsNullOrEmpty(numeBrand))
                    {
                        pastaCPU = await context.pasteProcesor.Include(p => p.Produs).Include(p => p.Produs.Categorie).Where(p => p.Produs.Brand.NumeBrand == numeBrand).ToListAsync();
                    }
                    pastaCPU = await context.pasteProcesor.Include(p => p.Produs).Include(p => p.Produs.Categorie).ToListAsync();
                    return Verificare<PastaCPU>(userId,pastaCPU);

                case "PlaciDeBaza":
                    List<Motherboard> placiDeBaza;
                    if (produsId != 0)
                        return context.placiDeBaza.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).SingleOrDefault(p => p.Produs.IdProdus == produsId);
                    if (!string.IsNullOrEmpty(numeBrand))
                    {
                        placiDeBaza = await context.placiDeBaza.Include(p => p.Produs).Include(p => p.Produs.Categorie).Where(p => p.Produs.Brand.NumeBrand == numeBrand).ToListAsync();
                    }
                    placiDeBaza = await context.placiDeBaza.Include(p => p.Produs).Include(p => p.Produs.Categorie).ToListAsync();
                    return Verificare<Motherboard>(userId, placiDeBaza);

                case "PlaciVideo":
                    List<GPU> placiVideo;
                    if (produsId != 0)
                        return context.placiVideo.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).SingleOrDefault(p => p.Produs.IdProdus == produsId);
                    if (!string.IsNullOrEmpty(numeBrand))
                    {
                        placiVideo = await context.placiVideo.Include(p => p.Produs).Include(p => p.Produs.Categorie).Where(p => p.Produs.Brand.NumeBrand == numeBrand).ToListAsync();
                    }
                    placiVideo = await context.placiVideo.Include(p => p.Produs).Include(p => p.Produs.Categorie).ToListAsync();
                    return Verificare<GPU>(userId, placiVideo);

                case "PlacuteRAM":
                    List<PlacutaRAM> placuteRAM;
                    if (produsId != 0)
                        return context.placuteRAM.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).SingleOrDefault(p => p.Produs.IdProdus == produsId);
                    if (!string.IsNullOrEmpty(numeBrand))
                    {
                        placuteRAM = await context.placuteRAM.Include(p => p.Produs).Include(p => p.Produs.Categorie).Where(p => p.Produs.Brand.NumeBrand == numeBrand).ToListAsync();
                    }
                    placuteRAM = await context.placuteRAM.Include(p => p.Produs).Include(p => p.Produs.Categorie).ToListAsync();
                    return Verificare<PlacutaRAM>(userId, placuteRAM);

                case "Procesoare":
                    List<CPU> procesoare;
                    if (produsId != 0)
                        return context.procesoare.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).SingleOrDefault(p => p.Produs.IdProdus == produsId);
                    if (!string.IsNullOrEmpty(numeBrand))
                    {
                        procesoare = await context.procesoare.Include(p => p.Produs).Include(p => p.Produs.Categorie).Where(p => p.Produs.Brand.NumeBrand == numeBrand).ToListAsync();
                    }
                    procesoare = await context.procesoare.Include(p => p.Produs).Include(p => p.Produs.Categorie).ToListAsync();
                    return Verificare<CPU>(userId, procesoare);

                case "SurseDeAlimentare":
                    List<PSU> surse;
                    if (produsId != 0)
                        return context.surse.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).SingleOrDefault(p => p.Produs.IdProdus == produsId);
                    if (!string.IsNullOrEmpty(numeBrand))
                    {
                        surse = await context.surse.Include(p => p.Produs).Include(p => p.Produs.Categorie).Where(p => p.Produs.Brand.NumeBrand == numeBrand).ToListAsync();
                    }
                    surse = await context.surse.Include(p => p.Produs).Include(p => p.Produs.Categorie).ToListAsync();
                    return Verificare(userId, surse);

                default:
                    List<UnitatiDeStocare> unitatiStocare;
                    if (produsId != 0)
                        return context.stocare.Include(p => p.Produs).Include(p => p.Produs.Categorie).Include(p => p.Produs.Brand).SingleOrDefault(p => p.Produs.IdProdus == produsId);
                    if (!string.IsNullOrEmpty(numeBrand))
                    {
                        unitatiStocare = await context.stocare.Include(p => p.Produs).Include(p => p.Produs.Categorie).Where(p => p.Produs.Brand.NumeBrand == numeBrand).ToListAsync();
                    }
                    unitatiStocare = await context.stocare.Include(p => p.Produs).Include(p => p.Produs.Categorie).ToListAsync();
                    return Verificare<UnitatiDeStocare>(userId, unitatiStocare);
            }

        }

        // Verifică produsele favoritate ale unui utilizator pentru fiecare categorie de produs
        public IndexProdusViewModel Verificare<T>(string userId,List<T> categorii)
        {
            List<Produs> produse = new List<Produs>();
            foreach (var produs in categorii)
            {
                if (produs is Carcasa carcasa)
                {
                    produse.Add(carcasa.Produs);
                    carcasa.Produs.ProdusFavorit = context.produseFavorite.
                        Any(p => p.ProdusId == carcasa.ProdusId &&
                        p.UtilizatorId == userId);
                }
                if (produs is PastaCPU pasta)
                {
                    produse.Add(pasta.Produs);
                    pasta.Produs.ProdusFavorit = context.produseFavorite.
                        Any(p => p.ProdusId == pasta.ProdusId &&
                        p.UtilizatorId == userId);
                }
                if (produs is Motherboard placaDeBaza)
                {
                    produse.Add(placaDeBaza.Produs);
                    placaDeBaza.Produs.ProdusFavorit = context.produseFavorite.
                       Any(p => p.ProdusId == placaDeBaza.ProdusId &&
                       p.UtilizatorId == userId);
                }
                if (produs is GPU placaVideo)
                {
                    produse.Add(placaVideo.Produs);
                    placaVideo.Produs.ProdusFavorit = context.produseFavorite.
                       Any(p => p.ProdusId == placaVideo.ProdusId &&
                       p.UtilizatorId == userId);
                }
                if (produs is PlacutaRAM placutaRAM)
                {
                    produse.Add(placutaRAM.Produs);
                    placutaRAM.Produs.ProdusFavorit = context.produseFavorite.
                       Any(p => p.ProdusId == placutaRAM.ProdusId &&
                       p.UtilizatorId == userId);
                }
                if (produs is CPU procesor)
                {
                    produse.Add(procesor.Produs);
                    procesor.Produs.ProdusFavorit = context.produseFavorite.
                      Any(p => p.ProdusId == procesor.ProdusId &&
                      p.UtilizatorId == userId);
                }
                if (produs is PSU sursa)
                {
                    produse.Add(sursa.Produs);
                    sursa.Produs.ProdusFavorit = context.produseFavorite.
                      Any(p => p.ProdusId == sursa.ProdusId &&
                      p.UtilizatorId == userId);
                }
                if (produs is UnitatiDeStocare unitate)
                {
                    produse.Add(unitate.Produs);
                    unitate.Produs.ProdusFavorit = context.produseFavorite.
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
        public void Asociere<T>(Produs produs,T categorie, AdaugaProdusViewModel model)
        {
            // Actualizăm informațiile produsului pe baza celor din ViewModel
            produs.Descriere = model.Produs.Descriere;
            produs.CategorieId = model.Produs.CategorieId;
            produs.Nume = model.Produs.Nume;
            produs.Pret = model.Produs.Pret;
            produs.Oferta = model.Produs.Oferta;
            produs.Stoc = model.Produs.Stoc;

            if (categorie is Motherboard placaDeBaza)
            {        
                produs.ImgLink = "Imagini/PlaciDeBaza/" + model.Produs.ImgLink;

                placaDeBaza.ProdusId = model.Produs.IdProdus;
                placaDeBaza.Socket = model.PlacaDeBaza.Socket;
                placaDeBaza.SloturiPCIe = model.PlacaDeBaza.SloturiPCIe;
                placaDeBaza.TipPortUSB = model.PlacaDeBaza.TipPortUSB;
                placaDeBaza.NrPorturiUSB = model.PlacaDeBaza.NrPorturiUSB;
                placaDeBaza.ConectoriSATA = model.PlacaDeBaza.ConectoriSATA;
                placaDeBaza.Chipset = model.PlacaDeBaza.Chipset;
                placaDeBaza.TipRAM = model.PlacaDeBaza.TipRAM;
                placaDeBaza.ConectoriM2 = model.PlacaDeBaza.ConectoriM2;
                placaDeBaza.FormFactor = model.PlacaDeBaza.FormFactor;
                placaDeBaza.NrSloturiRAM = model.PlacaDeBaza.NrSloturiRAM;
            }
            else if(categorie is Carcasa carcasa)
            {
                produs.ImgLink = "Imagini/Carcase/" + model.Produs.ImgLink;

                carcasa.ProdusId = model.Produs.IdProdus;
                carcasa.Format = model.Carcasa.Format;
                carcasa.NrVentilatoare = model.Carcasa.NrVentilatoare;
                carcasa.DeschidereLaterala = model.Carcasa.DeschidereLaterala;
                carcasa.TipCarcasa = model.Carcasa.TipCarcasa;
                carcasa.NrLocaseSloturiExtensii = model.Carcasa.NrLocaseSloturiExtensii;
                carcasa.Dimensiune = model.Carcasa.Dimensiune;
                carcasa.Greutate = model.Carcasa.Greutate;
                carcasa.Culoare = model.Carcasa.Culoare;
            }
            else if(categorie is CPU procesor)
            {
                produs.ImgLink = "Imagini/Procesoare/" + model.Produs.ImgLink;

                procesor.ProdusId = model.Produs.IdProdus;
                procesor.FrecventaBaza = model.Procesor.FrecventaBaza;
                procesor.NrNuclee = model.Procesor.NrNuclee;
                procesor.NrThreads = model.Procesor.NrThreads;
                procesor.PutereTermica = model.Procesor.PutereTermica;
                procesor.Socket = model.Procesor.Socket;
            }
            else if(categorie is GPU placaVideo)
            {
                produs.ImgLink = "Imagini/PlaciVideo/" + model.Produs.ImgLink;

                placaVideo.ProdusId = model.Produs.IdProdus;
                placaVideo.CapacitateMemorie = model.PlacaVideo.CapacitateMemorie;
                placaVideo.FrecventaMemorie = model.PlacaVideo.FrecventaMemorie;
                placaVideo.RezolutieMaxima = model.PlacaVideo.RezolutieMaxima;
                placaVideo.SistemRacire = model.PlacaVideo.SistemRacire;

            }
            else if(categorie is PastaCPU pasta)
            {
                produs.ImgLink = "Imagini/PastaProcesor/" + model.Produs.ImgLink;

                pasta.ProdusId = model.Produs.IdProdus;
                pasta.ConductivitateTermica = model.Pasta.ConductivitateTermica;
                pasta.RezistentaTermica = model.Pasta.RezistentaTermica;
                pasta.Cantitate = model.Pasta.Cantitate;
            }
            else if(categorie is PlacutaRAM placa)
            {
                produs.ImgLink = "Imagini/PlacuteRAM/" + model.Produs.ImgLink;

                placa.ProdusId = model.Produs.IdProdus;
                placa.Tip = model.PlacutaRAM.Tip;
                placa.Capacitate = model.PlacutaRAM.Capacitate;
                placa.Frecventa = model.PlacutaRAM.Frecventa;
                placa.Module = model.PlacutaRAM.Module;
            }
            else if(categorie is PSU sursa)
            {
                produs.ImgLink = "Imagini/SurseDeAlimentare/" + model.Produs.ImgLink;

                sursa.ProdusId = model.Produs.IdProdus;
                sursa.Putere = model.Sursa.Putere;
                sursa.NrVentilatoare = model.Sursa.NrVentilatoare;
                sursa.Alimentare = model.Sursa.Alimentare;
                sursa.Format = model.Sursa.Format;
                sursa.Greutate = model.Sursa.Greutate;
            }
            else if(categorie is UnitatiDeStocare stocare)
            {
                produs.ImgLink = "Imagini/UnitatiDeStocare/" + model.Produs.ImgLink;

                stocare.ProdusId = model.Produs.IdProdus;
                stocare.FormFactor = model.Stocare.FormFactor;
                stocare.Capacitate = model.Stocare.Capacitate;
                stocare.TipMemorie = model.Stocare.TipMemorie;
                stocare.RataTransferCitire = model.Stocare.RataTransferCitire;
                stocare.RataTransferScriere = model.Stocare .RataTransferScriere;
                stocare.Dimensiune = model.Stocare .Dimensiune;
                stocare.Greutate = model.Stocare .Greutate;
                stocare.LineUp = model.Stocare .LineUp;
                stocare.TipController = model.Stocare .TipController;
                stocare.Interfata = model.Stocare .Interfata;
            }
        }

        public AdaugaProdusViewModel CompletareModel(object produs, AdaugaProdusViewModel model)
        {
            switch (produs)
            {
                case Carcasa carcasa:
                    model.Carcasa = carcasa;
                    model.Produs = carcasa.Produs;
                    break;
                case PastaCPU pasta:
                    model.Pasta = pasta;
                    model.Produs = pasta.Produs;
                    break;
                case Motherboard placaDeBaza:
                    model.PlacaDeBaza = placaDeBaza;
                    model.Produs = placaDeBaza.Produs;
                    break;
                case GPU placaVideo:
                    model.PlacaVideo = placaVideo;
                    model.Produs = placaVideo.Produs;
                    break;
                case PlacutaRAM placutaRAM:
                    model.PlacutaRAM = placutaRAM;
                    model.Produs = placutaRAM.Produs;
                    break;
                case CPU procesor:
                    model.Procesor = procesor;
                    model.Produs = procesor.Produs;
                    break;
                case PSU sursa:
                    model.Sursa = sursa;
                    model.Produs = sursa.Produs;
                    break;
                case UnitatiDeStocare stocare:
                    model.Stocare = stocare;
                    model.Produs = stocare.Produs;
                    break;
            }

            return model;
        }
        public void SalvareProdus(AdaugaProdusViewModel model,Produs produs,MetodeExterne metodeExterne,BrandUser brand)
        {
            if (model.PlacaDeBaza != null)
            {
                SalvareProdusSpecific(model.PlacaDeBaza,produs,brand,model);
            }
            else if (model.Carcasa != null)
            {
                SalvareProdusSpecific(model.Carcasa,produs,brand,model);
            }
            else if (model.Procesor != null)
            {
                SalvareProdusSpecific(model.Procesor,produs,brand, model);
            }
            else if (model.Pasta != null)
            {
                SalvareProdusSpecific(model.Pasta,produs,brand, model);
            }
            else if (model.PlacaVideo != null)
            {
                SalvareProdusSpecific(model.PlacaVideo,produs,brand, model);
            }
            else if (model.PlacutaRAM != null)
            {
                SalvareProdusSpecific(model.PlacutaRAM, produs, brand, model);
            }
            else if (model.Sursa != null)
            {
                SalvareProdusSpecific(model.Sursa, produs, brand, model);
            }
            else if (model.Stocare != null)
            {
                SalvareProdusSpecific(model.Stocare, produs, brand, model);
            }
        }

        public void SalvareProdusSpecific(object produsSpecific,Produs produs,BrandUser brand, AdaugaProdusViewModel model)
        {
            if (produs.IdProdus != 0)
            {
                Asociere(produs,produsSpecific,model);
            }
            else
            {
                var product = produsSpecific as dynamic;
                product.Produs = produs;
                product.Produs.BrandId = brand.Id;
                AdaugareProdus(product);
                context.produse.Add(produs);
            }
        }

    }

}