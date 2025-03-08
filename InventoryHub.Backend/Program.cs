var builder = WebApplication.CreateBuilder(args);

// Configuración de CORS (más adelante se ajustará)
builder.Services.AddCors();

var app = builder.Build();

// Usar CORS con la política que permitiremos en el siguiente paso
app.UseCors(policy =>
     policy.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

// Actualizar la ruta a /api/productlist
app.MapGet("/api/productlist", () =>
{
    return new[]
    {
        new { Id = 1, Name = "Laptop", Price = 1200.50, Stock = 25 },
        new { Id = 2, Name = "Headphones", Price = 50.00, Stock = 100 }
    };
});

app.Run();
