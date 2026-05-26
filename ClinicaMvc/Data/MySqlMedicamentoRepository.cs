using ClinicaMVC.Models;
using MySqlConnector;

namespace ClinicaMVC.Data;

public class MySqlMedicamentoRepository(IConfiguration configuration) : IMedicamentoRepository
{
    private readonly string connectionString = configuration.GetConnectionString("FarmaciaDb")
        ?? throw new InvalidOperationException("Falta configurar la cadena de conexion FarmaciaDb.");

    public async Task<IReadOnlyList<Medicamento>> ObtenerTodosAsync()
    {
        const string sql = """
            SELECT id, nombre, descripcion, precio, stock, fecha_vencimiento, laboratorio, categoria
            FROM medicamentos
            ORDER BY nombre;
            """;

        var medicamentos = new List<Medicamento>();
        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();
        await using var command = new MySqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            medicamentos.Add(MapearMedicamento(reader));
        }

        return medicamentos;
    }

    public async Task<Medicamento?> ObtenerPorIdAsync(int id)
    {
        const string sql = """
            SELECT id, nombre, descripcion, precio, stock, fecha_vencimiento, laboratorio, categoria
            FROM medicamentos
            WHERE id = @id;
            """;

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();
        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        await using var reader = await command.ExecuteReaderAsync();

        return await reader.ReadAsync() ? MapearMedicamento(reader) : null;
    }

    public async Task CrearAsync(Medicamento medicamento)
    {
        const string sql = """
            INSERT INTO medicamentos (nombre, descripcion, precio, stock, fecha_vencimiento, laboratorio, categoria)
            VALUES (@nombre, @descripcion, @precio, @stock, @fechaVencimiento, @laboratorio, @categoria);
            """;

        await EjecutarAsync(sql, medicamento);
    }

    public async Task ActualizarAsync(Medicamento medicamento)
    {
        const string sql = """
            UPDATE medicamentos
            SET nombre = @nombre,
                descripcion = @descripcion,
                precio = @precio,
                stock = @stock,
                fecha_vencimiento = @fechaVencimiento,
                laboratorio = @laboratorio,
                categoria = @categoria
            WHERE id = @id;
            """;

        await EjecutarAsync(sql, medicamento);
    }

    public async Task EliminarAsync(int id)
    {
        const string sql = "DELETE FROM medicamentos WHERE id = @id;";

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();
        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        await command.ExecuteNonQueryAsync();
    }

    private async Task EjecutarAsync(string sql, Medicamento medicamento)
    {
        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();
        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", medicamento.Id);
        command.Parameters.AddWithValue("@nombre", medicamento.Nombre.Trim());
        command.Parameters.AddWithValue("@descripcion", medicamento.Descripcion.Trim());
        command.Parameters.AddWithValue("@precio", medicamento.Precio);
        command.Parameters.AddWithValue("@stock", medicamento.Stock);
        command.Parameters.AddWithValue("@fechaVencimiento", medicamento.FechaVencimiento.Date);
        command.Parameters.AddWithValue("@laboratorio", medicamento.Laboratorio.Trim());
        command.Parameters.AddWithValue("@categoria", medicamento.Categoria.Trim());
        await command.ExecuteNonQueryAsync();
    }

    private static Medicamento MapearMedicamento(MySqlDataReader reader)
    {
        return new Medicamento
        {
            Id = reader.GetInt32("id"),
            Nombre = reader.GetString("nombre"),
            Descripcion = reader.GetString("descripcion"),
            Precio = reader.GetDecimal("precio"),
            Stock = reader.GetInt32("stock"),
            FechaVencimiento = reader.GetDateTime("fecha_vencimiento"),
            Laboratorio = reader.GetString("laboratorio"),
            Categoria = reader.GetString("categoria")
        };
    }
}
