using Microsoft.AspNetCore.Mvc;
using Enerfit.Models;

namespace Enerfit.Controllers
{
    public class HomeController : Controller
    {
        // GET: Página de bienvenida
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // GET: Login
        [HttpGet]
        public IActionResult InicioSesion()
        {
            return View();
        }

        // POST: Procesar login
        [HttpPost]
        public IActionResult InicioSesion(string nombreUsuario, string contrasenia)
        {
            bool usuarioValido = (nombreUsuario == "test" && contrasenia == "1234");

            if (usuarioValido == true)
            {
                return RedirectToAction("Index"); // vuelve al home después del login
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrecta";
                return View();
            }
        }

        
        public IActionResult IrARegistro()
{
    return RedirectToAction("Registro", "Home");
}

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        // POST: Procesar registro
        [HttpPost]
        public IActionResult Registro(string nombreUsuario, string contrasenia, string nombre, string apellido, string email)
        {
            // Guardar en la base de datos si aplica
            return RedirectToAction("InicioSesion");
        }
         public IActionResult Alimentacion()
        {
            return View();
        }
         public IActionResult Entrenamiento()
        {
            return View();
        }
        public IActionResult PlanesPorObjetivo1()
        {
            return View();
        }
    }
}
