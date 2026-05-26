Farmacia Vida Sana
==================

Sistema web basico desarrollado con ASP.NET MVC y MySQL para la Evaluacion Sumativa II de Desarrollo Web con ASP.NET y MySQL.

Funcionalidades
---------------

- Pagina de inicio.
- Catalogo publico de medicamentos.
- Pagina de contacto.
- Barra de navegacion funcional.
- Diseno responsivo con CSS personalizado.
- Panel de administracion protegido por login.
- CRUD completo de medicamentos: crear, listar, editar y eliminar.
- Conexion a base de datos MySQL.
- Validaciones basicas en formularios.
- Manejo de sesion para restringir el panel administrador.

Base de datos
-------------

La aplicacion usa la base de datos `farmacia_db` y la tabla `medicamentos`.

Campos de la tabla:

- `id`
- `nombre`
- `descripcion`
- `precio`
- `stock`
- `fecha_vencimiento`
- `laboratorio`
- `categoria`

Los dos atributos agregados son `laboratorio` y `categoria`.

La aplicacion crea automaticamente la base de datos, la tabla y datos iniciales al iniciar, usando la cadena de conexion de `ClinicaMvc/appsettings.json`.

Tambien se incluye el archivo `database.sql` para revisar o ejecutar la estructura manualmente desde MySQL Workbench.

Acceso administrador
--------------------

- Usuario: `admin`
- Contrasena: `admin123`

Ejecucion
---------

1. Verificar que el servicio MySQL este iniciado.
2. Revisar la cadena de conexion en `ClinicaMvc/appsettings.json`.
3. Ejecutar:

```powershell
dotnet run --project ClinicaMvc\ClinicaMvc.csproj
```

4. Abrir la URL indicada por la consola.

Rutas principales
-----------------

- `/` pagina de inicio.
- `/Home/Catalogo` catalogo publico.
- `/Home/Contacto` contacto.
- `/AdminMedicamentos` panel administrador.
