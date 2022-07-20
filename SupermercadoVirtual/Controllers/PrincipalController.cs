using Microsoft.AspNetCore.Mvc;
using SupermercadoVirtual.Models;

namespace SupermercadoVirtual.Controllers
{
    [Route("principal")]
    public class PrincipalController : Controller
    {
        [Route("")]
        [Route("~/")]
        [Route("index")]
        public IActionResult Index()
        {
            ProductoModel pm = new ProductoModel();
            ViewBag.productos = pm.getTodo();//Lo resuelvo pasandole la lista por viewBag, pero sin mezclar lógica de negocios en controlador.
            return View();
        }
    }
}
