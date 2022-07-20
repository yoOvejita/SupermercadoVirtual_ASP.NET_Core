using System.Collections.Generic;
using System.Linq;

namespace SupermercadoVirtual.Models
{
    public class ProductoModel
    {
        //Clase de apoyo para poder contar, rápidamente, con una lista de productos y con métodos de consulta.
        private List<Producto> productos;
        public ProductoModel()
        {
            productos = new List<Producto>()
            {
                new Producto
                {
                    id = 1,
                    nombre = "Atún VanCamps",
                    precio = 12,
                    imagen = "atun.jpg"
                },
                new Producto
                {
                    id = 2,
                    nombre = "Queso menonita",
                    precio = 45,
                    imagen = "queso.jpg"
                }
            };
        }
        public List<Producto> getTodo()
        {
            return productos;
        }
        public Producto getById(int id)
        {
            return productos.Single(prod => prod.id == id);
        }
    }
}
