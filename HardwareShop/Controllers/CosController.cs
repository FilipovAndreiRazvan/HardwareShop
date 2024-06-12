using HardwareShop.Models;
using HardwareShop.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HardwareShop.Controllers
{
    public class CosController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Cos

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var cosDb = context.cos.Where(c => c.utilizator == userId).ToList();
            var cos = new CosCumparaturiViewModel();

            if (cosDb.Count == 0)
            {
                cos.Id = 0;
                return View(cos);
            }
            else
            {
                cos.Id = 1;
            }

            foreach (var item in cosDb)
            {
                switch (item.categorie)
                {
                    case "carcase":
                        {
                            var carcasa = context.carcase.SingleOrDefault(c => c.Id == item.idProdus && item.utilizator == userId);
                            if (carcasa != null)
                            {
                                Dictionary<int, Carcasa> carcasa1 = new Dictionary<int, Carcasa>
                                {
                                    { item.nrBuc,carcasa }
                                };
                                cos.carcase.Add(carcasa1);
                            }

                        }
                        break;
                    case "placiDeBaza":
                        {
                            var placaDeBaza = context.placiDeBaza.SingleOrDefault(p => p.Id == item.idProdus && item.utilizator == userId);
                            if (placaDeBaza != null)
                            {
                                Dictionary<int, Motherboard> placaDeBaza1 = new Dictionary<int, Motherboard>
                                {
                                    { item.nrBuc,placaDeBaza }
                                };
                                cos.motherboard.Add(placaDeBaza1);
                            }

                        }
                        break;
                    case "procesoare":
                        {
                            var procesor = context.procesoare.SingleOrDefault(p => p.Id == item.idProdus && item.utilizator == userId);
                            if (procesor != null)
                            {
                                Dictionary<int, CPU> procesor1 = new Dictionary<int, CPU>
                                {
                                    { item.nrBuc,procesor }
                                };
                                cos.cpu.Add(procesor1);
                            }

                        }
                        break;
                    case "placiVideo":
                        {
                            var placaVideo = context.placiVideo.SingleOrDefault(p => p.Id == item.idProdus && item.utilizator == userId);
                            if (placaVideo != null)
                            {
                                Dictionary<int, GPU> placaVideo1 = new Dictionary<int, GPU>()
                                {
                                    {item.nrBuc,placaVideo }
                                };
                                cos.gpu.Add(placaVideo1);
                            }

                        }
                        break;
                    case "placuteRAM":
                        {
                            var placutaRAM = context.placuteRAM.SingleOrDefault(p => p.Id == item.idProdus && item.utilizator == userId);
                            if (placutaRAM != null)
                            {
                                Dictionary<int, PlacutaRAM> placutaRAM1 = new Dictionary<int, PlacutaRAM>()
                                {
                                    {item.nrBuc,placutaRAM}
                                };
                                cos.placuteRAM.Add(placutaRAM1);
                            }

                        }
                        break;
                    case "unitatiDeStocare":
                        {
                            var unitateStocare = context.stocare.SingleOrDefault(p => p.Id == item.idProdus && item.utilizator == userId);
                            if (unitateStocare != null)
                            {
                                Dictionary<int, UnitatiDeStocare> unitateStocare1 = new Dictionary<int, UnitatiDeStocare>()
                                {
                                 { item.nrBuc,unitateStocare }
                                };

                                cos.unitatiDeStocare.Add(unitateStocare1);
                            }

                        }
                        break;
                    case "surseDeAlimentare":
                        {
                            var sursa = context.surse.SingleOrDefault(p => p.Id == item.idProdus && item.utilizator == userId);
                            if (sursa != null)
                            {
                                Dictionary<int, PSU> sursa1 = new Dictionary<int, PSU>()
                                {
                                    { item.nrBuc,sursa }
                                };
                                cos.psu.Add(sursa1);
                            }

                        }
                        break;
                    case "pastaCPU":
                        {
                            var pastaCPU = context.pasteProcesor.SingleOrDefault(p => p.Id == item.idProdus && item.utilizator == userId);
                            if (pastaCPU != null)
                            {
                                Dictionary<int, PastaCPU> pastaCPU1 = new Dictionary<int, PastaCPU>()
                                {
                                    {item.nrBuc,pastaCPU}
                                };
                                cos.pastaCPU.Add(pastaCPU1);
                            }

                        }
                        break;
                }
            }

            Session["produseCos"] = cosDb;
            return View(cos);
        }
        public ActionResult Adauga(int idProdus, string categorie, int nrBuc, string viewName)
        {
<<<<<<< HEAD
            var cosDb = context.cos.SingleOrDefault(c => c.idProdus == idProdus && c.categorie == categorie);
            if (cosDb != null)
            {
                cosDb.nrBuc += nrBuc;
            }
            else
=======

            for (int i = 0; i < nrBuc; i++)
>>>>>>> parent of 65f7645 (Modificari)
            {
                var cos = new CosCumparaturi();
                cos.categorie = categorie;
                cos.idProdus = idProdus;
                var userId = User.Identity.GetUserId();
                cos.utilizator = userId;
                cos.nrBuc = nrBuc;
                context.cos.Add(cos);
            }
<<<<<<< HEAD



            switch (categorie)
            {
                case "carcase":
                    Carcasa carcasa = context.carcase.Single(p => p.Id == idProdus);
                    carcasa.stoc -= nrBuc;
                    if (carcasa.stoc < 0)
                    {
                        return RedirectToAction("Vizualizare", "Carcase", new { id = carcasa.Id });
                    }

                    break;
                case "placiDeBaza":
                    context.placiDeBaza.Single(p => p.Id == idProdus).stoc -= nrBuc;
                    break;
                case "procesoare":
                    context.procesoare.Single(p => p.Id == idProdus).stoc -= nrBuc;
                    break;
                case "placiVideo":
                    context.placiVideo.Single(p => p.Id == idProdus).stoc -= nrBuc;
                    break;
                case "placuteRAM":
                    context.placuteRAM.Single(p => p.Id == idProdus).stoc -= nrBuc;
                    break;
                case "unitatiDeStocare":
                    context.stocare.Single(p => p.Id == idProdus).stoc -= nrBuc;
                    break;
                case "surseDeAlimentare":
                    context.surse.Single(p => p.Id == idProdus).stoc -= nrBuc;
                    break;
                case "pastaCPU":
                    {
                        context.pasteProcesor.Single(p => p.Id == idProdus).stoc -= nrBuc;
                    }
                    break;
            }
=======
>>>>>>> parent of 65f7645 (Modificari)
            context.SaveChanges();
            if (viewName == "cosCumparaturi")
            {
                return RedirectToAction("Index", "Cos", new { adaugaProdusCos = true });
            }
            return RedirectToAction("Index", "Home", new { adaugaProdusCos = true });
        }

        public ActionResult Sterge(int idProdus, string categorie, int nrBuc)
        {
            var userId = User.Identity.GetUserId();
            CosCumparaturi cos;
            if (userId == null)
            {
                cos = context.cos.FirstOrDefault(c => c.idProdus == idProdus && c.categorie == categorie && c.utilizator == userId);
            }
            else
            {
                cos = context.cos.FirstOrDefault(c => c.idProdus == idProdus && c.categorie == categorie && c.utilizator == userId);
            }

            switch (categorie)
            {
                case "carcase":
                    context.carcase.Single(p => p.Id == idProdus).stoc += nrBuc;
                    break;
                case "placiDeBaza":
                    context.placiDeBaza.Single(p => p.Id == idProdus).stoc += nrBuc;
                    break;
                case "procesoare":
                    context.procesoare.Single(p => p.Id == idProdus).stoc += nrBuc;
                    break;
                case "placiVideo":
                    context.placiVideo.Single(p => p.Id == idProdus).stoc += nrBuc;
                    break;
                case "placuteRAM":
                    context.placuteRAM.Single(p => p.Id == idProdus).stoc += nrBuc;
                    break;
                case "unitatiDeStocare":
                    context.stocare.Single(p => p.Id == idProdus).stoc += nrBuc;
                    break;
                case "surseDeAlimentare":
                    context.surse.Single(p => p.Id == idProdus).stoc += nrBuc;
                    break;
                case "pastaCPU":
                    {
                        context.pasteProcesor.Single(p => p.Id == idProdus).stoc += nrBuc;
                    }
                    break;
            }
            cos.nrBuc -= nrBuc;
            if (cos.nrBuc == 0)
            {
                context.cos.Remove(cos);
            }


            context.SaveChanges();
            return RedirectToAction("Index", "Cos");
        }

    }
}