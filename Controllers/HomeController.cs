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

            if (usuarioValido)
            {
                return RedirectToAction("Index"); // vuelve al home después del login
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrecta";
                return View();
            }
        }

        // GET: Registro
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Procesar registro
        [HttpPost]
        public IActionResult Register(string nombreUsuario, string contrasenia, string nombre, string apellido, string email)
        {
            // Guardar en la base de datos si aplica
            return RedirectToAction("InicioSesion");
        }
    }
}
