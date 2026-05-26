using ClinicaMVC.Models;

namespace ClinicaMVC.Data;

public interface IMedicamentoRepository
{
    Task<IReadOnlyList<Medicamento>> ObtenerTodosAsync();
    Task<Medicamento?> ObtenerPorIdAsync(int id);
    Task CrearAsync(Medicamento medicamento);
    Task ActualizarAsync(Medicamento medicamento);
    Task EliminarAsync(int id);
}
