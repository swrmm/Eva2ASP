using ClinicaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaMVC.Controllers
{
    public class PacienteController : Controller
    {
        private const string RegistrarView = "~/Views/Registrar.cshtml";
        private static int totalPacientes;
        private static int totalRecaudado;

        public IActionResult Registrar()
        {
            CargarResumen();
            return View(RegistrarView, new Paciente());
        }

        [HttpPost]
        public IActionResult Registrar(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                paciente.CostoBase = ObtenerCostoBase(paciente.TipoAtencion);
                paciente.Descuento = paciente.TienePrevision ? (int)(paciente.CostoBase * 0.30) : 0;
                paciente.TotalPagar = paciente.CostoBase - paciente.Descuento;

                totalPacientes++;
                totalRecaudado += paciente.TotalPagar;
            }

            CargarResumen();
            return View(RegistrarView, paciente);
        }

        private static int ObtenerCostoBase(int tipoAtencion)
        {
            return tipoAtencion switch
            {
                1 => 10000,
                2 => 25000,
                3 => 15000,
                _ => 0
            };
        }

        private void CargarResumen()
        {
            ViewBag.TotalPacientes = totalPacientes;
            ViewBag.TotalRecaudado = totalRecaudado;
        }
    }
}
