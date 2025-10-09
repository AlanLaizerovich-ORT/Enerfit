using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Enerfit_Proyecto.Models;

namespace Enerfit_Proyecto.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}
