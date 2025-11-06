using Microsoft.AspNetCore.Mvc;
using Enerfit.Models;
using Enerfit; 

namespace Enerfit.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult InicioSesion() => View();

        [HttpPost]
        public IActionResult IniciarSesion(string nombreUsuario, string contrasenia)
        {
            Usuario usuario = BD.ObtenerUsuario(nombreUsuario, contrasenia);

            if (usuario != null)
            {
                HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View("InicioSesion");
            }
        }
        public IActionResult IrARegistro()
{
    return RedirectToAction("Registro");
}
        

        // ✅ GET Registro
        [HttpGet]
        public IActionResult Registro() => View();

        // ✅ POST Registro — (nombre igual que el del formulario)
        [HttpPost]
        public IActionResult Registro(string nombreUsuario, string contrasenia, string nombre, string apellido, string email, string sexo)
        {
            var nuevoUsuario = new Usuario
            {
                Nombre = nombreUsuario,
                Contrasenia = contrasenia
            };

            // 🔹 Insertar usuario y obtener su Id autogenerado
            int nuevoId = BD.AgregarUsuario(nuevoUsuario);

            var nuevoPerfil = new Perfil
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Sexo = sexo,
                IDUsuario = nuevoId
            };

            BD.AgregarPerfil(nuevoPerfil);

            return RedirectToAction("InicioSesion");
        }
        

        public IActionResult Alimentacion() => View();
        public IActionResult Entrenamiento() => View();
        public IActionResult PlanesPorObjetivo1() => View();
        public IActionResult Videos() => View();
        public IActionResult RutinasPorZona() => View();
        public IActionResult Hombros() => View();
        public IActionResult Piernas() => View();
        public IActionResult Bicep() => View();
        public IActionResult Tricep() => View();
        public IActionResult Abdomen() => View();
        public IActionResult Pecho() => View();
        public IActionResult Volumen() => View();
        public IActionResult Deficit() => View();
    }
}
