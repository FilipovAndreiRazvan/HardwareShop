using System.Web.Mvc;

namespace HardwareShop.Controllers
{
    public class ComandaController : Controller
    {
        // GET: Comanda
        [Authorize(Roles = "Client")]
        public ActionResult DetaliiComanda()
        {

            return View();
        }
    }
}