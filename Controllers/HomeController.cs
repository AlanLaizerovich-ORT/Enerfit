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
        public IActionResult IniciarSesion(string nombreUsuario, string contrasenia)
        {
             Usuario usuario = BD.ObtenerUsuario(nombreUsuario, contrasenia);

     if (usuario != null)
         {
        //  Usuario encontrado → iniciar sesión
        HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
        return RedirectToAction("Index", "Home");
        }
        else
     {
        //  Usuario no existe → mostrar error
        ViewBag.Error = "Usuario o contraseña incorrectos.";
        return View("Login");
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
        public IActionResult Registro(string nombreUsuario, string contrasenia, string nombre, string apellido, string email, string sexo, int IDPlanPorObj, int IDPlanPerso, int IDUsuario)
        {
            var nuevoUsuario = new Usuario
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
                IDPlanPorObj = IDPlanPorObj,
                IDPlanPerso = IDPlanPerso,
                IDUsuario = nuevoUsuario.IdUsuario
           
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
          public IActionResult Videos()
        {
            return View();
        }
    }
}
