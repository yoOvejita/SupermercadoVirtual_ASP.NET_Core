using Microsoft.AspNetCore.Mvc;
using SupermercadoVirtual.Helper;
using SupermercadoVirtual.Models;
using System.Collections.Generic;
using System.Linq;

namespace SupermercadoVirtual.Controllers
{
    public class CarritoController : Controller
    {
        public IActionResult Index()
        {
            List<Elemento> carrito = SesionHelper.GetJsonToObjeto<List<Elemento>>(HttpContext.Session, "carrito");
            if(carrito != null && carrito.Count > 0)
            {
                //Le pasamos la lista de elementos
                ViewBag.carrito = carrito;
                //Le pasamos el total que lo obtenemos desde controlador (también podría ser obtenido en vista)
                ViewBag.total = carrito.Sum(elem => elem.producto.precio * elem.cantidad);
            }
            else
            {
                ViewBag.carrito = new List<Elemento>();
                ViewBag.total = 0;
            }
            
            return View();
        }

        [Route("comprar/{id}")]
        public IActionResult Comprar(int id)
        {
            ProductoModel pm = new ProductoModel();//Aca estan los métodos de consulta sobre el conjunto de productos

            if (SesionHelper.GetJsonToObjeto<List<Elemento>>(HttpContext.Session, "carrito") == null) {
                List<Elemento> carrito = new List<Elemento>();
                carrito.Add(new Elemento { producto = pm.getById(id), cantidad = 1 });
                SesionHelper.SetObjetoToJson(HttpContext.Session, "carrito", carrito);
            }else
            {
                List<Elemento> carrito = SesionHelper.GetJsonToObjeto<List<Elemento>>(HttpContext.Session, "carrito");
                var indice = getIndice(id);
                if (indice != -1)//caso1: que quiera añadir un producto que ya está en el carrito
                    carrito[indice].cantidad++;
                else//caso2: que quiera añadir un producto que no está en el carrito
                    carrito.Add(new Elemento { producto = pm.getById(id), cantidad = 1 });

                SesionHelper.SetObjetoToJson(HttpContext.Session, "carrito", carrito);
            }
            return RedirectToAction("Index");
        }
        private int getIndice(int id)//Exactamente el código mostrado en clases
        {
            List<Elemento> carrito = SesionHelper.GetJsonToObjeto<List<Elemento>>(HttpContext.Session, "carrito");
            for (int i = 0; i < carrito.Count; i++)
            {
                if (carrito[i].producto.id == id)
                    return i;
            }
            return -1;
        }
        [Route("eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            List<Elemento> carrito = SesionHelper.GetJsonToObjeto<List<Elemento>>(HttpContext.Session, "carrito");
            int indice = getIndice(id);
            if(indice != -1)
            {
                carrito.RemoveAt(indice);
                SesionHelper.SetObjetoToJson(HttpContext.Session, "carrito", carrito);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Vaciar()
        {
            List<Elemento> carrito = SesionHelper.GetJsonToObjeto<List<Elemento>>(HttpContext.Session, "carrito");
            ViewBag.total = carrito.Sum(elem => elem.producto.precio * elem.cantidad);
            carrito = new List<Elemento>();
            SesionHelper.SetObjetoToJson(HttpContext.Session, "carrito", carrito);
            return View("Listo");
        }
    }
}
