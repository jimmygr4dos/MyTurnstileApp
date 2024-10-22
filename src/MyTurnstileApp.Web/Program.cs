// src/MyTurnstileApp.Web/Program.cs
using MyTurnstileApp.Application;
using MyTurnstileApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Añadir servicios al contenedor.
builder.Services.AddControllersWithViews(); // Asegúrate de que esto está incluido
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// Configurar otras opciones de servicios si es necesario

var app = builder.Build();

// Configuración del middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configurar las rutas de los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
