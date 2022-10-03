using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        public ActionResult GetAll()
        {

            ML.Producto producto = new ML.Producto();

            producto.fabricante = new ML.Fabricante();

            ML.Result result = BL.Producto.GetAll();

            if(result.correct)
            {
                producto.productos = result.Objects;
            }
            else
            {
                result.correct = false;
            }
            return View(producto);
        }
        [HttpGet]
        public IActionResult Form()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
         
            if(producto.IdProducto==0)
            {
                ML.Result result  = BL.Producto.Add(producto);

                if(result.correct)
                {
                    ViewBag.Mensaje = "Registro exitoso";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error"+result.ErrorMessage;
                }
                return View ("Modal");
            }
            return View("Modal");

        }
    }
}
