var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Paciente/Registrar");
}

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Paciente}/{action=Registrar}/{id?}");

app.Run();
