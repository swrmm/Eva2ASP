Clínica MVC
Aplicación web simple en ASP.NET MVC para registrar pacientes y calcular el valor a pagar por una atención médica.

¿Qué hace?
Permite ingresar los datos de un paciente:

Nombre
Edad
Si tiene previsión
Tipo de atención
Luego calcula automáticamente:

Costo base de la atención
Descuento si tiene previsión
Total a pagar
Cantidad de pacientes atendidos
Total recaudado en el día
Tipos de atención
La aplicación maneja tres tipos de atención:

Consulta general: $10.000
Urgencia: $25.000
Examen: $15.000
Si el paciente tiene previsión, se aplica un 30% de descuento.

Cómo ejecutar la aplicación
Abrir el proyecto en Visual Studio.
Compilar la solución.
Ejecutar la aplicación con el botón de inicio.
Se abrirá la página de registro de paciente.
También se puede ejecutar desde consola con:

dotnet run
Cómo usarla
Escribir el nombre del paciente.
Ingresar la edad.
Seleccionar si tiene previsión.
Seleccionar el tipo de atención.
Presionar el botón Generar Boleta.
La aplicación mostrará el total a pagar y actualizará el resumen del día.
Archivos principales
Paciente.cs: contiene el modelo del paciente.
PacienteController.cs: contiene la lógica para registrar pacientes y calcular valores.
Registrar.cshtml: contiene el formulario y muestra la boleta.
