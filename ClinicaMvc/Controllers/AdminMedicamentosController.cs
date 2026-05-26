using ClinicaMVC.Data;
using ClinicaMVC.Filters;
using ClinicaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaMVC.Controllers;

[AdminSessionAuthorize]
public class AdminMedicamentosController(IMedicamentoRepository repository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var medicamentos = await repository.ObtenerTodosAsync();
        return View(medicamentos);
    }

    public IActionResult Crear()
    {
        return View(new Medicamento());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Medicamento medicamento)
    {
        if (!ModelState.IsValid)
        {
            return View(medicamento);
        }

        await repository.CrearAsync(medicamento);
        TempData["Mensaje"] = "Medicamento creado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Editar(int id)
    {
        var medicamento = await repository.ObtenerPorIdAsync(id);
        return medicamento is null ? NotFound() : View(medicamento);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, Medicamento medicamento)
    {
        if (id != medicamento.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(medicamento);
        }

        await repository.ActualizarAsync(medicamento);
        TempData["Mensaje"] = "Medicamento actualizado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Eliminar(int id)
    {
        var medicamento = await repository.ObtenerPorIdAsync(id);
        return medicamento is null ? NotFound() : View(medicamento);
    }

    [HttpPost, ActionName("Eliminar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmarEliminar(int id)
    {
        await repository.EliminarAsync(id);
        TempData["Mensaje"] = "Medicamento eliminado correctamente.";
        return RedirectToAction(nameof(Index));
    }
}
