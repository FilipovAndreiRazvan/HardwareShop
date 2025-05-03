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
using HardwareShop.Service;

public class ServiciiController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly GestionareProduseService _gestionareProduse;
    private readonly GestionareModeleService _gestionareModele;

    //dependency injection pentru context si clasa ajutatoare metodeExterne
    public ServiciiController(ApplicationDbContext context,GestionareProduseService gestionareProduse, GestionareModeleService gestionareModele)
    {
        _context = context;
        _gestionareProduse = gestionareProduse;
        _gestionareModele = gestionareModele;
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
        

        var model = await _gestionareProduse.ListareProduse(_context,userId,categorie);

        if (model == null)
        {
            return View(); // Afișează view-ul când nu există produse
        }

        model.categorieProduse = categorie;

        return View(model);
    }

    // Afișează detalii despre un produs specific
    public async Task<ActionResult> Vizualizare(int id, string categorie)
    {
        var userId = User.Identity.GetUserId();
        var model = await _gestionareProduse.ListaProduse(userId, categorie, produsId: id);
       
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
        model.Produs.Categorie = await _context.categorie.SingleOrDefaultAsync(c => c.Id == model.Produs.CategorieId);
        await _gestionareProduse.SalvareProdus(userId,model);
       

        if (model.Produs.IdProdus != 0)
        {
            TempData["Mesaj"] = "ProdusModificat";
        }
        else
        {
            TempData["Mesaj"] = "ProdusAdaugat";
        }

        // Salvează produsul fie ca nou, fie actualizat

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
         var model = await _gestionareModele.CompletareModel(categorie,idProdus);
        return View(model);
    }
    public async Task<ActionResult> Sterge(int id,string categorie)
    {
        await _gestionareProduse.StergeProdus(_context, id);
        TempData["Mesaj"] = "ProdusSters";
        return RedirectToAction("Index", new { categorie = categorie });
    }
}
