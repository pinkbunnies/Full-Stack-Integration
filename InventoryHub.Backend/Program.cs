using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Agregar MemoryCache para implementar caching
builder.Services.AddMemoryCache();

// Agregar servicios CORS
builder.Services.AddCors();

var app = builder.Build();

// Configuración de CORS para permitir cualquier origen
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

// Obtener el servicio de caché
var cache = app.Services.GetRequiredService<IMemoryCache>();

// Endpoint que devuelve la lista de productos con objeto 'Category' anidado
app.MapGet("/api/productlist", () =>
{
    // Utiliza la caché para reducir llamadas redundantes
    return cache.GetOrCreate("ProductList", entry =>
    {
        // Establecer expiración de la caché a 5 minutos
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

        // Código generado con ayuda de Copilot para estructurar la respuesta JSON
        return new[]
        {
            new
            {
                Id = 1,
                Name = "Laptop",
                Price = 1200.50,
                Stock = 25,
                Category = new { Id = 101, Name = "Electronics" }
            },
            new
            {
                Id = 2,
                Name = "Headphones",
                Price = 50.00,
                Stock = 100,
                Category = new { Id = 102, Name = "Accessories" }
            }
        };
    });
});

app.Run();
