using Microsoft.AspNetCore.Mvc;
using Enerfit.Models;
using Enerfit;
using Microsoft.AspNetCore.Http;

namespace Enerfit.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UsuarioID") == null)
                return RedirectToAction("InicioSesion");

            ViewBag.UsuarioNombre = HttpContext.Session.GetString("UsuarioNombre");
            return View();
        }

      [HttpGet]
public IActionResult InicioSesion()
{

    if (HttpContext.Session.GetInt32("UsuarioID") != null)
        return RedirectToAction("Perfil");

    return View();
}

        [HttpPost]
public IActionResult IniciarSesion(string nombreUsuario, string contrasenia)
{
   
    if (HttpContext.Session.GetInt32("UsuarioID") != null)
        return RedirectToAction("Index");

    Usuario usuario = BD.ObtenerUsuario(nombreUsuario, contrasenia);

    if (usuario != null)
    {
       
        HttpContext.Session.SetInt32("UsuarioID", usuario.IdUsuario);
        HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);

       
        return RedirectToAction("Index");
    }
    else
    {
        ViewBag.Error = "Usuario o contraseña incorrectos.";
        return View("InicioSesion");
    }
}
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("InicioSesion");
        }

        public IActionResult IrARegistro() => RedirectToAction("Registro");

        [HttpGet]
      

        [HttpPost]
        [HttpPost]
[HttpPost]
public IActionResult Registro(string nombreUsuario, string contrasenia, string nombre, string apellido, string email, string sexo)
{
    if (string.IsNullOrEmpty(nombre))
    {
        ViewBag.Error = "El nombre es obligatorio.";
        return View();
    }

    var nuevoUsuario = new Usuario
    {
        Nombre = nombre,
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

    ViewBag.Mensaje = "Usuario registrado correctamente.";
    return RedirectToAction("InicioSesion");
}


        

      
        public IActionResult Alimentacion() => ValidarSesion(View());
        public IActionResult Entrenamiento() => ValidarSesion(View());
        public IActionResult PlanesPorObjetivo1() => ValidarSesion(View());
        public IActionResult Videos() => ValidarSesion(View());
        public IActionResult RutinasPorZona() => ValidarSesion(View());
        public IActionResult Hombros() => ValidarSesion(View());
        public IActionResult Piernas() => ValidarSesion(View());
        public IActionResult Bicep() => ValidarSesion(View());
        public IActionResult Tricep() => ValidarSesion(View());
        public IActionResult Abdomen() => ValidarSesion(View());
        public IActionResult Pecho() => ValidarSesion(View());
        public IActionResult Volumen() => ValidarSesion(View());
        public IActionResult Deficit() => ValidarSesion(View());
        public IActionResult Progreso() => ValidarSesion(View());
        public IActionResult Comunidad() => ValidarSesion(View());
        public IActionResult HealthBot() => ValidarSesion(View());
        public IActionResult CrearPlanEntrenamiento() => ValidarSesion(View());
        public IActionResult CrearPlanAlimentacion() => ValidarSesion(View());
        public IActionResult Tutorial() => ValidarSesion(View());
        public IActionResult VerPlanPersonalizado() => ValidarSesion(View());
            public IActionResult VerPlanEntrenamiento() => ValidarSesion(View());
       

        public IActionResult Perfil()
        {
            int? idUsuario = HttpContext.Session.GetInt32("UsuarioID");

            if (idUsuario == null)
                return RedirectToAction("InicioSesion");

            Perfil perfil = BD.ObtenerPerfilPorUsuario(idUsuario.Value);

            if (perfil == null)
            {
                ViewBag.Error = "No se encontró el perfil del usuario.";
                return View();
            }

            return View(perfil);
        }
          private IActionResult ValidarSesion(IActionResult vista)
        {
            if (HttpContext.Session.GetInt32("UsuarioID") == null)
                return RedirectToAction("InicioSesion");

            return vista;
        }
        [HttpGet]
public IActionResult EditarPerfil()
{
    int? idUsuario = HttpContext.Session.GetInt32("UsuarioID");

    if (idUsuario == null)
        return RedirectToAction("InicioSesion");

    Perfil perfil = BD.ObtenerPerfilPorUsuario(idUsuario.Value);

    if (perfil == null)
    {
        ViewBag.Error = "No se encontró el perfil del usuario.";
        return RedirectToAction("Perfil");
    }

    return View(perfil);
}

[HttpPost]
public IActionResult EditarPerfil(Perfil perfilActualizado)
{
    int? idUsuario = HttpContext.Session.GetInt32("UsuarioID");

    if (idUsuario == null)
        return RedirectToAction("InicioSesion");

    perfilActualizado.IDUsuario = idUsuario.Value;

    BD.ActualizarPerfil(perfilActualizado);

    ViewBag.Mensaje = " Perfil actualizado correctamente.";
    return RedirectToAction("Perfil");
}



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
    // ====== MENÚ PRINCIPAL ======
if (input == "menu" || input == "inicio" || input == "empezar" || input == "opciones")
{
    string menu =
        "Hola, soy Fitty. Decime qué querés hacer:\n\n" +
        "1) Entrenamiento\n" +
        "2) Alimentación\n" +
        "3) Planes por objetivo\n" +
        "4) Crear rutina o dieta\n" +
        "5) Perfil\n" +
        "6) Videos\n" +
        "7) Bienestar y descanso\n" +
        "8) Tutorial del asistente\n" +
        "0) Nada por ahora\n\n" +
        "Escribí el número o la palabra clave.";

    return (menu, null);
}
// ====== OPCIONES POR NÚMERO ======
switch (input)
{
    case "1":
        return ("Te llevo a la sección de entrenamiento.", "/Home/Entrenamiento");

    case "2":
        return ("Ingresando a la sección de alimentación.", "/Home/Alimentacion");

    case "3":
        return ("Mostrando planes por objetivo.", "/Home/PlanesPorObjetivo1");

    case "4":
        return ("Decime si querés crear una rutina o una dieta.", null);

    case "5":
        return ("Abriendo tu perfil.", "/Home/Perfil");

    case "6":
        return ("Mostrando videos de ejercicios.", "/Home/Videos");

    case "7":
        return ("Podés consultarme sobre descanso, estrés o motivación.", null);

    case "8":
        return ("Abriendo el tutorial del asistente.", "/Home/Tutorial");

    case "0":
        return ("De acuerdo. Si necesitás algo, escribime de nuevo.", null);
}



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
// =======================
//     LISTA DE RECETAS
// =======================
[HttpGet]
[HttpGet][HttpGet]


// =======================
//     CREAR RECETA
// =======================
[HttpGet]
public IActionResult CrearReceta()
{
    return ValidarSesion(View());
}

[HttpPost]
public IActionResult CrearReceta(Recetas receta)
{
    int id = BD.CrearReceta(receta);
return RedirectToAction("VerReceta", new { id = id });
}

// =======================
//     VER UNA RECETA
// =======================
[HttpGet]
[HttpGet]
public IActionResult VerReceta(int id)
{
    var receta = BD.ObtenerReceta(id);

    if (receta == null)
    {
        // Si no encuentra la receta, redirige de nuevo a la vista de Recetas
        ViewBag.Error = "La receta no existe.";
        return RedirectToAction("Recetas");
    }

    return View(receta);  // Aquí pasas el modelo correctamente
}



// =======================
//     EDITAR RECETA
// =======================
[HttpGet]
public IActionResult EditarReceta(int id)
{
    if (HttpContext.Session.GetInt32("UsuarioID") == null)
        return RedirectToAction("InicioSesion");

    var receta = BD.ObtenerReceta(id);
    return View("EditarReceta", receta);
}

[HttpPost]
public IActionResult EditarReceta(Recetas receta)
{
    BD.EditarReceta(receta);
    return RedirectToAction("Recetas");
}

// =======================
//     BORRAR
// =======================
public IActionResult BorrarReceta(int id)
{
    BD.BorrarReceta(id);
    return RedirectToAction("Recetas");
}
public IActionResult VerRecetas()
{
    return RedirectToAction("Recetas");
}
[HttpGet]
public IActionResult CalculadoraIMC()
{
    if (HttpContext.Session.GetInt32("UsuarioID") == null)
        return RedirectToAction("InicioSesion");

    return View();
}

    }
    
}
