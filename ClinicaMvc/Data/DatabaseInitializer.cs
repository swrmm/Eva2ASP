using ClinicaMVC.Models;
using MySqlConnector;

namespace ClinicaMVC.Data;

public static class DatabaseInitializer
{
    public static async Task EnsureCreatedAsync(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("FarmaciaDb")
            ?? throw new InvalidOperationException("Falta configurar la cadena de conexion FarmaciaDb.");

        var builder = new MySqlConnectionStringBuilder(connectionString);
        var databaseName = builder.Database;

        if (string.IsNullOrWhiteSpace(databaseName))
        {
            throw new InvalidOperationException("La cadena de conexion debe incluir el nombre de la base de datos.");
        }

        builder.Database = string.Empty;
        await using (var connection = new MySqlConnection(builder.ConnectionString))
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(
                $"CREATE DATABASE IF NOT EXISTS {EscaparIdentificador(databaseName)} CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;",
                connection);
            await command.ExecuteNonQueryAsync();
        }

        builder.Database = databaseName;
        await using (var connection = new MySqlConnection(builder.ConnectionString))
        {
            await connection.OpenAsync();
            await CrearTablaAsync(connection);
            await SembrarDatosAsync(connection);
        }
    }

    private static async Task CrearTablaAsync(MySqlConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS medicamentos (
                id INT NOT NULL AUTO_INCREMENT,
                nombre VARCHAR(120) NOT NULL,
                descripcion VARCHAR(500) NOT NULL,
                precio DECIMAL(10,2) NOT NULL,
                stock INT NOT NULL,
                fecha_vencimiento DATE NOT NULL,
                laboratorio VARCHAR(120) NOT NULL,
                categoria VARCHAR(80) NOT NULL,
                creado_en TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                PRIMARY KEY (id)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
            """;

        await using var command = new MySqlCommand(sql, connection);
        await command.ExecuteNonQueryAsync();
    }

    private static async Task SembrarDatosAsync(MySqlConnection connection)
    {
        await using (var countCommand = new MySqlCommand("SELECT COUNT(*) FROM medicamentos;", connection))
        {
            var total = Convert.ToInt32(await countCommand.ExecuteScalarAsync());
            if (total > 0)
            {
                return;
            }
        }

        var medicamentos = new[]
        {
            new Medicamento
            {
                Nombre = "Paracetamol 500 mg",
                Descripcion = "Analgesico y antipiretico para dolor leve o fiebre.",
                Precio = 1990,
                Stock = 80,
                FechaVencimiento = DateTime.Today.AddYears(2),
                Laboratorio = "Chilefarma",
                Categoria = "Analgesicos"
            },
            new Medicamento
            {
                Nombre = "Ibuprofeno 400 mg",
                Descripcion = "Antiinflamatorio no esteroidal de uso comun.",
                Precio = 3490,
                Stock = 45,
                FechaVencimiento = DateTime.Today.AddMonths(18),
                Laboratorio = "Andes Lab",
                Categoria = "Antiinflamatorios"
            },
            new Medicamento
            {
                Nombre = "Loratadina 10 mg",
                Descripcion = "Antihistaminico para sintomas de alergia.",
                Precio = 2790,
                Stock = 35,
                FechaVencimiento = DateTime.Today.AddMonths(20),
                Laboratorio = "Salud Sur",
                Categoria = "Antialergicos"
            }
        };

        const string sql = """
            INSERT INTO medicamentos (nombre, descripcion, precio, stock, fecha_vencimiento, laboratorio, categoria)
            VALUES (@nombre, @descripcion, @precio, @stock, @fechaVencimiento, @laboratorio, @categoria);
            """;

        foreach (var medicamento in medicamentos)
        {
            await using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@nombre", medicamento.Nombre);
            command.Parameters.AddWithValue("@descripcion", medicamento.Descripcion);
            command.Parameters.AddWithValue("@precio", medicamento.Precio);
            command.Parameters.AddWithValue("@stock", medicamento.Stock);
            command.Parameters.AddWithValue("@fechaVencimiento", medicamento.FechaVencimiento.Date);
            command.Parameters.AddWithValue("@laboratorio", medicamento.Laboratorio);
            command.Parameters.AddWithValue("@categoria", medicamento.Categoria);
            await command.ExecuteNonQueryAsync();
        }
    }

    private static string EscaparIdentificador(string value)
    {
        return $"`{value.Replace("`", "``")}`";
    }
}
