using Microsoft.AspNetCore.Mvc;
using Enerfit.Models;
using Enerfit;
using Microsoft.AspNetCore.Http;

namespace Enerfit.Controllers
{
    public class HomeController : Controller
    {
        // ===== VISTAS PRINCIPALES =====
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

        public IActionResult IrARegistro() => RedirectToAction("Registro");

        [HttpGet]
        public IActionResult Registro() => View();

        [HttpPost]
        public IActionResult Registro(string nombreUsuario, string contrasenia, string nombre, string apellido, string email, string sexo)
        {
            var nuevoUsuario = new Usuario
            {
                Nombre = nombreUsuario,
                Contrasenia = contrasenia
            };

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

        // ===== VISTAS DE SECCIONES =====
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
        public IActionResult Progreso() => View();
        public IActionResult Comunidad() => View();
        public IActionResult Perfil() => View();

        // ===== HEALTHBOT =====
        public IActionResult HealthBot() => View();

        // --- Método AJAX que recibe el mensaje del usuario ---
        [HttpPost]
        public JsonResult GetResponse(string userMessage)
        {
            var (reply, redirect) = GetBotResponse(userMessage);
            return Json(new { reply, redirect });
        }

        // --- Lógica del chatbot ---
        private (string reply, string redirect) GetBotResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return ("No te entendí 😅. Probá escribirme algo más claro.", null);

            input = input.ToLower();

            // --- SALUDOS ---
            if (input.Contains("hola") || input.Contains("buenas") || input.Contains("hey"))
                return ("¡Hola! Soy tu asistente de salud Enerfit 🤖 ¿Querés hablar de *entrenamiento*, *nutrición* o *hábitos saludables*?", null);

            if (input.Contains("cómo estás") || input.Contains("que tal"))
                return ("¡Mejor que nunca! 💪 Estoy listo para ayudarte con tu bienestar.", null);

            // --- ENTRENAMIENTO ---
            if (input.Contains("entrenamiento") || input.Contains("rutina") || input.Contains("ejercicio") || input.Contains("gimnasio"))
                return ("Entrenar regularmente mejora tu fuerza y energía ⚡. Te llevo a la sección de entrenamiento 👉", "/Home/Entrenamiento");

            if (input.Contains("pesas") || input.Contains("músculo") || input.Contains("fuerza"))
                return ("💪 Te recomiendo ejercicios compuestos como sentadillas, peso muerto y press de banca.", null);

            if (input.Contains("cardio") || input.Contains("correr") || input.Contains("caminar"))
                return ("El cardio mejora tu resistencia y salud cardiovascular 🏃. Probá 30 minutos diarios.", null);

            // --- ALIMENTACIÓN ---
            if (input.Contains("nutrición") || input.Contains("alimentación") || input.Contains("comida") || input.Contains("dieta"))
                return ("🍎 Comer bien es clave para tu progreso. Te llevo a la sección de alimentación 👉", "/Home/Alimentacion");

            if (input.Contains("agua") || input.Contains("hidratar"))
                return ("Tomar agua mejora tu rendimiento y concentración 💧. ¡Mínimo 2 litros al día!", null);

            if (input.Contains("proteína") || input.Contains("pollo") || input.Contains("carne") || input.Contains("batido"))
                return ("La proteína ayuda a reparar tus músculos 💪. Podés incluir pollo, huevos, yogur o legumbres.", null);

            if (input.Contains("vegetales") || input.Contains("verdura") || input.Contains("fruta"))
                return ("¡Excelente! 🍉 Frutas y verduras aportan vitaminas esenciales para tu energía diaria.", null);

            // --- MOTIVACIÓN / HÁBITOS ---
            if (input.Contains("motivación") || input.Contains("desmotivado") || input.Contains("ánimo"))
                return ("No te rindas 💥. Los grandes cambios comienzan con pequeñas acciones diarias.", null);

            if (input.Contains("descanso") || input.Contains("dormir"))
                return ("Dormir bien ayuda a tus músculos a recuperarse 💤. Apuntá a 7-8 horas por noche.", null);

            if (input.Contains("estrés") || input.Contains("ansiedad"))
                return ("Respirá profundo 🌿. El ejercicio y una buena alimentación ayudan a reducir el estrés.", null);

            // --- CIERRES ---
            if (input.Contains("gracias") || input.Contains("grac"))
                return ("¡De nada! 😄 Recordá que cada paso cuenta hacia tus objetivos.", null);

            if (input.Contains("adiós") || input.Contains("chau") || input.Contains("nos vemos"))
                return ("¡Hasta pronto! Seguí moviéndote y cuidando tu cuerpo 🧡.", null);

            // --- RESPUESTA POR DEFECTO ---
            return ("No entendí eso 😅. Podés hablarme de *entrenamiento*, *nutrición* o *hábitos saludables*.", null);
        }
    }
}
