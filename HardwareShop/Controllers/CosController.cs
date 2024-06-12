using HardwareShop.Models;
using Microsoft.AspNet.Identity;
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
            var cos = new CosCumparaturi();

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
                                cos.carcase.Add(carcasa);
                            }

                        }
                        break;
                    case "placiDeBaza":
                        {
                            var placaDeBaza = context.placiDeBaza.SingleOrDefault(p => p.Id == item.idProdus && item.utilizator == userId);
                            if (placaDeBaza != null)
                            {
                                cos.motherboard.Add(placaDeBaza);
                            }

                        }
                        break;
                    case "procesoare":
                        {
                            var procesor = context.procesoare.SingleOrDefault(p => p.Id == item.idProdus && item.utilizator == userId);
                            if (procesor != null)
                            {
                                cos.cpu.Add(procesor);
                            }

                        }
                        break;
                    case "placiVideo":
                        {
                            var placaVideo = context.placiVideo.SingleOrDefault(p => p.Id == item.idProdus && item.utilizator == userId);
                            if (placaVideo != null)
                            {
                                cos.gpu.Add(placaVideo);
                            }

                        }
                        break;
                    case "placuteRAM":
                        {
                            var placutaRAM = context.placuteRAM.SingleOrDefault(p => p.Id == item.idProdus && item.utilizator == userId);
                            if (placutaRAM != null)
                            {
                                cos.placuteRAM.Add(placutaRAM);
                            }

                        }
                        break;
                    case "unitatiDeStocare":
                        {
                            var unitateStocare = context.stocare.SingleOrDefault(p => p.Id == item.idProdus && item.utilizator == userId);
                            if (unitateStocare != null)
                            {
                                cos.unitatiDeStocare.Add(unitateStocare);
                            }

                        }
                        break;
                    case "surseDeAlimentare":
                        {
                            var sursa = context.surse.SingleOrDefault(p => p.Id == item.idProdus && item.utilizator == userId);
                            if (sursa != null)
                            {
                                cos.psu.Add(sursa);
                            }

                        }
                        break;
                    default:
                        {
                            var pastaCPU = context.pasteProcesor.SingleOrDefault(p => p.Id == item.idProdus && item.utilizator == userId);
                            if (pastaCPU != null)
                            {
                                cos.pastaCPU.Add(pastaCPU);
                            }

                        }
                        break;
                }
            }
            return View(cos);
        }
        public ActionResult Adauga(int id, string categorie, int nrBuc)
        {

                var cos = new CosCumparaturi();
                cos.categorie = categorie;
                cos.idProdus = id;
                var userId = User.Identity.GetUserId();
                cos.utilizator = userId;


                switch (categorie)
                {
                    case "carcase":
                        context.carcase.Single(p => p.Id == id).stoc--;
                        break;
                    case "placiDeBaza":
                        context.placiDeBaza.Single(p => p.Id == id).stoc--;
                        break;
                    case "procesoare":
                        context.procesoare.Single(p => p.Id == id).stoc--;
                        break;
                    case "placiVideo":
                        context.placiVideo.Single(p => p.Id == id).stoc--;
                        break;
                    case "placuteRAM":
                        context.placuteRAM.Single(p => p.Id == id).stoc--;
                        break;
                    case "unitatiDeStocare":
                        context.stocare.Single(p => p.Id == id).stoc--;
                        break;
                    case "surseDeAlimentare":
                        context.surse.Single(p => p.Id == id).stoc--;
                        break;
                    default:
                        {
                            context.pasteProcesor.Single(p => p.Id == id).stoc--;
                        }
                        break;
                }
                context.cos.Add(cos);
            context.SaveChanges();
            return RedirectToAction("Index", "Home", new { adaugaProdusCos = true });
        }

        public ActionResult Sterge(int id, string categorie)
        {
            var userId = User.Identity.GetUserId();
            CosCumparaturi cos;
            if (userId == null)
            {
                cos = context.cos.FirstOrDefault(c => c.idProdus == id && c.categorie == categorie && c.utilizator == userId);
            }
            else
            {
                cos = context.cos.FirstOrDefault(c => c.idProdus == id && c.categorie == categorie && c.utilizator == userId);
            }

            switch (categorie)
            {
                case "carcase":
                    context.carcase.Single(p => p.Id == id).stoc++;
                    break;
                case "placiDeBaza":
                    context.placiDeBaza.Single(p => p.Id == id).stoc++;
                    break;
                case "procesoare":
                    context.procesoare.Single(p => p.Id == id).stoc++;
                    break;
                case "placiVideo":
                    context.placiVideo.Single(p => p.Id == id).stoc++;
                    break;
                case "placuteRAM":
                    context.placuteRAM.Single(p => p.Id == id).stoc++;
                    break;
                case "unitatiDeStocare":
                    context.stocare.Single(p => p.Id == id).stoc++;
                    break;
                case "surseDeAlimentare":
                    context.surse.Single(p => p.Id == id).stoc++;
                    break;
                default:
                    {
                        context.pasteProcesor.Single(p => p.Id == id).stoc++;
                    }
                    break;
            }
            context.cos.Remove(cos);

            context.SaveChanges();
            return RedirectToAction("Index", "Cos");
        }

    }
}