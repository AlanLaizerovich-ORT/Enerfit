using Microsoft.AspNetCore.Mvc;
using Enerfit.Models;
using TP0_INTROBD; // 👈 para usar la clase BD

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

        // POST: Procesar login con BD (Dapper)
        [HttpPost]
        public IActionResult InicioSesion(string nombreUsuario, string contrasenia)
        {
            var usuarioValido = BD.ObtenerUsuario(nombreUsuario, contrasenia);

            if (usuarioValido != null)
            {
                // Guardamos el nombre en sesión si querés
                HttpContext.Session.SetString("Usuario", usuarioValido.Nombre);
                return RedirectToAction("Index");
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

        // POST: Procesar registro con BD (Dapper)
        [HttpPost]
        public IActionResult Registro(string nombreUsuario, string contrasenia, string nombre, string apellido, string email, string sexo)
        {
            var nuevoUsuario = new Usuarios
            {
                Nombre = nombreUsuario,
                Contrasenia = contrasenia
            };

            // Guardar usuario
            BD.AgregarUsuario(nuevoUsuario);

            // Crear perfil (podés ajustarlo si tu BD asigna IdUsuario automáticamente)
            var nuevoPerfil = new Perfil
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Sexo = sexo,
                IdUsuario = nuevoUsuario.IdUsuario
            };

            BD.AgregarPerfil(nuevoPerfil);

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
