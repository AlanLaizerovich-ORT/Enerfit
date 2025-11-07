using Microsoft.AspNetCore.Mvc;
using Enerfit.Models;
using Enerfit;
using Microsoft.AspNetCore.Http;

namespace Enerfit.Controllers
{
    public class HomeController : Controller
    {
        // ======== VISTAS PRINCIPALES ========
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

        // ======== VISTAS DE SECCIONES ========
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
        public IActionResult HealthBot() => View();

        // ======== CHATBOT ========
        [HttpPost]
        public JsonResult GetResponse(string userMessage)
        {
            var (reply, redirect) = GetBotResponse(userMessage);
            return Json(new { reply, redirect });
        }

       private (string reply, string redirect) GetBotResponse(string input)
{
    if (string.IsNullOrWhiteSpace(input))
        return ("No te entendí 😅. Probá escribirme algo más claro.", null);

    input = input.ToLower();

    // ======== SALUDOS ========
    if (input.Contains("hola") || input.Contains("buenas") || input.Contains("hey"))
        return ("¡Hola! Soy Fitty  ¿Querés hablar de *entrenamiento*, *rutinas* o *alimentación*?", null);

    if (input.Contains("cómo estás") || input.Contains("como estas"))
        return ("¡De maravilla y listo para ayudarte a cumplir tus metas! 💪", null);

    if (input.Contains("quién sos") || input.Contains("qué sos") || input.Contains("que sos"))
        return ("Soy tu asistente virtual Enerfit 🤖. Estoy para guiarte en tus rutinas y alimentación 💚.", null);

    // ======== ENTRENAMIENTO ========
    if (input.Contains("entrenamiento") || input.Contains("ejercicio") || input.Contains("gimnasio"))
        return ("Entrenar con constancia es clave 💪. Te llevo a la sección de entrenamiento 👉", "/Home/Entrenamiento");

    if (input.Contains("crear rutina") || input.Contains("nueva rutina") || input.Contains("armar rutina"))
        return ("Perfecto 💥 podés crear y ver tus rutinas personalizadas acá 👉", "/Home/RutinasPorZona");

    if (input.Contains("rutina") || input.Contains("mi rutina") || input.Contains("rutinas"))
        return ("Podés consultar tus rutinas por zona muscular acá 👉", "/Home/RutinasPorZona");

    if (input.Contains("pierna") || input.Contains("piernas"))
        return ("🔥 Día de piernas, ¡vamos con todo! Te llevo a los ejercicios 👉", "/Home/Piernas");

    if (input.Contains("pecho"))
        return ("💪 Pecho fuerte y definido. Mirá esta rutina 👉", "/Home/Pecho");

    if (input.Contains("hombro") || input.Contains("hombros"))
        return ("🦾 Fortalecer hombros mejora la postura. Te llevo 👉", "/Home/Hombros");

    if (input.Contains("bicep") || input.Contains("bíceps"))
        return ("💪 Hora de marcar esos bíceps. Te llevo 👉", "/Home/Bicep");

    if (input.Contains("tricep") || input.Contains("tríceps"))
        return ("💥 Tríceps poderosos, ¡vamos! 👉", "/Home/Tricep");

    if (input.Contains("abdomen") || input.Contains("abdominales"))
        return ("🔥 A marcar el abdomen. Te llevo 👉", "/Home/Abdomen");

    if (input.Contains("videos") || input.Contains("tutorial") || input.Contains("ver ejercicios"))
        return ("Podés ver los videos de ejercicios en movimiento acá 👉", "/Home/Videos");

    if (input.Contains("planes") || input.Contains("objetivo"))
        return ("¿Buscás *volumen* o *déficit*? Te muestro los planes 👉", "/Home/PlanesPorObjetivo1");

    // ======== ALIMENTACIÓN ========
    if (input.Contains("alimentación") || input.Contains("nutrición") || input.Contains("comida") || input.Contains("dieta"))
        return ("🍎 La nutrición es clave para tus resultados. Te llevo a la sección 👉", "/Home/Alimentacion");

    if (input.Contains("crear dieta") || input.Contains("nueva dieta"))
        return ("Perfecto 🥗 Podés crear tu plan nutricional personalizado acá 👉", "/Home/Alimentacion");

    if (input.Contains("receta") || input.Contains("recetas"))
        return ("Podés explorar recetas saludables y ricas acá 👉", "/Home/Alimentacion");

    if (input.Contains("ingrediente") || input.Contains("ingredientes"))
        return ("🍅 Los ingredientes importan. Revisá la sección de alimentación 👉", "/Home/Alimentacion");

    if (input.Contains("volumen"))
        return ("🍚 Para ganar masa, una dieta con superávit calórico es ideal. Te llevo 👉", "/Home/Volumen");

    if (input.Contains("déficit") || input.Contains("deficit") || input.Contains("bajar de peso"))
        return ("🥦 Para definir o bajar grasa, mantené un déficit calórico saludable. Te llevo 👉", "/Home/Deficit");

    if (input.Contains("agua") || input.Contains("hidratar"))
        return ("💧 Recordá tomar al menos 2 litros de agua por día para rendir al máximo.", null);

    // ======== DESCANSO Y MOTIVACIÓN ========
    if (input.Contains("descanso") || input.Contains("dormir"))
        return ("Dormir bien 💤 es clave para la recuperación muscular. Apuntá a 7–8 horas por noche.", null);

    if (input.Contains("estrés") || input.Contains("ansiedad"))
        return ("Respirá profundo 🌿. Entrenar o salir a caminar puede ayudarte a liberar tensiones.", null);

    if (input.Contains("motivación") || input.Contains("ánimo") || input.Contains("desmotivado"))
        return ("No te rindas 💥. Cada paso cuenta, incluso los más chicos. ¡Seguí adelante!", null);

    // ======== PERFIL Y AYUDA ========
    if (input.Contains("perfil") || input.Contains("mis datos") || input.Contains("mi cuenta"))
        return ("Podés editar tu información personal o cerrar sesión desde acá 👉", "/Home/Perfil");

    if (input.Contains("ayuda") || input.Contains("necesito ayuda") || input.Contains("no sé"))
        return ("Estoy para ayudarte 💚. Podés preguntarme sobre *entrenamiento*, *rutinas* o *alimentación*.", null);

    if (input.Contains("error") || input.Contains("no funciona"))
        return ("😅 Si algo no anda bien, podés volver a intentar o revisar tu perfil 👉", "/Home/Perfil");

    // ======== SECCIONES FUTURAS ========
    if (input.Contains("progreso") || input.Contains("seguimiento"))
        return ("📈 La sección de progreso estará disponible próximamente. ¡Pronto podrás registrar tus avances!", null);

    if (input.Contains("comunidad") || input.Contains("foro") || input.Contains("personas"))
        return ("🌍 La comunidad Enerfit está en desarrollo. Pronto podrás conectarte con otros usuarios 💬", null);

    // ======== CIERRES ========
    if (input.Contains("gracias"))
        return ("¡De nada! 😄 Recordá que la constancia es tu mejor aliada.", null);

    if (input.Contains("adiós") || input.Contains("chau") || input.Contains("nos vemos"))
        return ("👋 ¡Hasta la próxima! Seguí moviéndote y cuidando tu cuerpo 🧡", null);

    // ======== DEFAULT ========
    return ("No entendí eso 😅. Podés hablarme de *entrenamiento*, *rutinas* o *alimentación*.", null);
}
    }
}
