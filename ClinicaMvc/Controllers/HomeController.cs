using ClinicaMVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaMVC.Controllers;

public class HomeController(IMedicamentoRepository repository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var medicamentos = await repository.ObtenerTodosAsync();
        return View(medicamentos.Take(3).ToList());
    }

    public async Task<IActionResult> Catalogo()
    {
        var medicamentos = await repository.ObtenerTodosAsync();
        return View(medicamentos);
    }

    public IActionResult Contacto()
    {
        return View();
    }
}
