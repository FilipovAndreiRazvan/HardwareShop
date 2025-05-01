using HardwareShop.Models;
using HardwareShop.ViewModels;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

public class ServiciiController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly MetodeExterne _metodeExterne;

    //dependency injection pentru context si clasa ajutatoare metodeExterne
    public ServiciiController(ApplicationDbContext context, MetodeExterne metodeExterne)
    {
        _context = context;
        _metodeExterne = metodeExterne;
    }

    // Afișăm lista de produse filtrate după categorie și brand (dacă userul e furnizor)
    public async Task<ActionResult> Index(string categorie, bool index = false, bool paginaOferte = false)
    {
        if (paginaOferte)
        {
            return RedirectToAction("Index", "Home"); // Redirecționează spre pagina principală
        }

        if (index)
        {
            TempData["Mesaj"] = null; // Resetează mesajul temporar
        }

        var userId = User.Identity.GetUserId();
        var rezultat = await _metodeExterne.ListaProduse(userId, categorie);

        if (rezultat == null)
        {
            return View(); // Afișează view-ul când nu există produse
        }

        var model = (IndexProdusViewModel)rezultat;

        if (User.IsInRole("Furnizor"))
        {
            // Dacă utilizatorul e furnizor, filtrează doar produsele brandului său
            var brandEntity = await _context.branduri.SingleOrDefaultAsync(b => b.User.Id == userId);
            var brand = brandEntity.NumeBrand;

            if (brand != null)
            {
                rezultat = await _metodeExterne.ListaProduse(userId, categorie, numeBrand: brand);
                model = (IndexProdusViewModel)rezultat;
            }
            else
            {
                throw new HttpException(404, "Brand inexistent!");
            }
        }

        if (model == null)
        {
            throw new HttpException(404, "Model inexistent!");
        }

        model.categorieProduse = categorie;

        return View(model);
    }

    // Afișează detalii despre un produs specific
    public async Task<ActionResult> Vizualizare(int id, string categorie)
    {
        var userId = User.Identity.GetUserId();
        var produs = await _metodeExterne.ListaProduse(userId, categorie, produsId: id);

        var model = _metodeExterne.CompletareModel(produs, new AdaugaProdusViewModel());

        if (model == null)
        {
            throw new HttpException(404, "Model inexistent!");
        }

        return View(model);
    }

    // Salvează un produs nou sau actualizează unul existent
    [HttpPost]
    public async Task<ActionResult> Salvare(AdaugaProdusViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model); // Dacă modelul nu este valid, returnează același view
        }

        var userId = User.Identity.GetUserId();
        var brand = await _context.branduri.SingleOrDefaultAsync(b => b.User.Id == userId);
        var categorie = await _context.categorie.SingleOrDefaultAsync(c => c.Id == model.Produs.CategorieId);
        var produs = await _context.produse.SingleOrDefaultAsync(p => p.IdProdus == model.Produs.IdProdus);

        model.Produs.Categorie = categorie;

        if (model.Produs.IdProdus != 0)
        {
            TempData["Mesaj"] = "ProdusModificat";
            //await ViewAdauga(categorie.Nume, produs.IdProdus);
        }
        else
        {
            //await ViewAdauga(categorie.Nume, null);
            TempData["Mesaj"] = "ProdusAdaugat";
        }

        // Salvează produsul fie ca nou, fie actualizat
        if (produs == null)
        {
            _metodeExterne.SalvareProdus(model, model.Produs, _metodeExterne, brand);
        }
        else
        {
            _metodeExterne.SalvareProdus(model, produs, _metodeExterne, brand);
        }

        try
        {
            await _context.SaveChangesAsync(); // Salvează modificările în baza de date
        }
        catch (DbEntityValidationException e)
        {
            Console.WriteLine(e); // Tratează erorile de validare la salvare
        }

        return RedirectToAction("Index", new { categorie = model.Produs.Categorie.Nume });
    }

    // View-ul pentru adăugarea sau modificarea unui produs
    public async Task<ActionResult> ViewAdauga(string categorie, int? idProdus)
    {
        var model = new AdaugaProdusViewModel
        {
            Produs = new Produs()
        };

        if (idProdus.HasValue)
        {
            // Cazul de modificare produs existent
            var produs = await _context.produse.FirstOrDefaultAsync(p => p.IdProdus == idProdus);
            if (produs == null)
            {
                throw new HttpException(404, "Produs inexistent!");
            }

            model.Produs = produs;

            // Completăm modelul în funcție de tipul produsului
            switch (categorie)
            {
                case "Carcase":
                    model.Carcasa = await _context.carcase.Include(c => c.Produs).SingleOrDefaultAsync(c => c.ProdusId == produs.IdProdus);
                    break;
                case "PlaciDeBaza":
                    model.PlacaDeBaza = await _context.placiDeBaza.Include(c => c.Produs).SingleOrDefaultAsync(c => c.ProdusId == produs.IdProdus);
                    break;
                case "PlaciVideo":
                    model.PlacaVideo = await _context.placiVideo.FirstOrDefaultAsync(c => c.ProdusId == idProdus);
                    break;
                case "PlacuteRAM":
                    model.PlacutaRAM = await _context.placuteRAM.FirstOrDefaultAsync(c => c.ProdusId == idProdus);
                    break;
                case "Procesoare":
                    model.Procesor = await _context.procesoare.FirstOrDefaultAsync(c => c.ProdusId == idProdus);
                    break;
                case "SurseDeAlimentare":
                    model.Sursa = await _context.surse.FirstOrDefaultAsync(c => c.ProdusId == idProdus);
                    break;
                case "UnitatiStocare":
                    model.Stocare = await _context.stocare.FirstOrDefaultAsync(c => c.ProdusId == idProdus);
                    break;
                case "PastaProcesor":
                    model.Pasta = await _context.pasteProcesor.FirstOrDefaultAsync(c => c.ProdusId == idProdus);
                    break;
                default:
                    return HttpNotFound();
            }

            return View(model);
        }
        else
        {
            // Cazul de adăugare produs nou (setare categorii implicite)
            switch (categorie)
            {
                case "Carcase":
                    model.Carcasa = new Carcasa();
                    model.Produs.CategorieId = 1;
                    break;
                case "PlaciDeBaza":
                    model.PlacaDeBaza = new Motherboard();
                    model.Produs.CategorieId = 2;
                    break;
                case "PlaciVideo":
                    model.PlacaVideo = new GPU();
                    model.Produs.CategorieId = 3;
                    break;
                case "PlacuteRAM":
                    model.PlacutaRAM = new PlacutaRAM();
                    model.Produs.CategorieId = 4;
                    break;
                case "Procesoare":
                    model.Procesor = new CPU();
                    model.Produs.CategorieId = 5;
                    break;
                case "SurseDeAlimentare":
                    model.Sursa = new PSU();
                    model.Produs.CategorieId = 6;
                    break;
                case "UnitatiStocare":
                    model.Stocare = new UnitatiDeStocare();
                    model.Produs.CategorieId = 7;
                    break;
                case "PastaProcesor":
                    model.Pasta = new PastaCPU();
                    model.Produs.CategorieId = 8;
                    break;
                default:
                    throw new HttpException(404, "Categorie inexistenta!");
            }

            return View(model);
        }
    }

    // Șterge produsul și toate referințele lui din coș și lista de favorite
    public async Task<ActionResult> Sterge(int id)
    {
        var produs = await _context.produse.Include(p => p.Categorie).FirstOrDefaultAsync(p => p.IdProdus == id);

        if (produs == null)
        {
            throw new HttpException(404, "Produs inexistent!");
        }

        var categorie = produs.Categorie.Nume;

        // Elimină produsul din coșuri și lista de favorite
        var cos = await _context.cos.Where(c => c.ProdusId == produs.IdProdus).ToListAsync();
        var listaFavorite = await _context.produseFavorite.Where(c => c.ProdusId == produs.IdProdus).ToListAsync();

        _context.cos.RemoveRange(cos);
        _context.produseFavorite.RemoveRange(listaFavorite);
        _context.produse.Remove(produs);

        await _context.SaveChangesAsync();

        TempData["Mesaj"] = "ProdusSters";

        return RedirectToAction("Index", new { categorie = categorie });
    }
}
