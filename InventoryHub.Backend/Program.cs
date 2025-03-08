using Microsoft.Extensions.Caching.Memory; // Import the memory caching library

var builder = WebApplication.CreateBuilder(args);

// Add MemoryCache service to enable in-memory caching
builder.Services.AddMemoryCache();

// Add CORS (Cross-Origin Resource Sharing) services to control API access
builder.Services.AddCors();

var app = builder.Build();

// Configure CORS to allow requests from any origin, method, and header
app.UseCors(policy =>
    policy.AllowAnyOrigin() // Allows requests from any domain (not restricted)
          .AllowAnyMethod() // Allows all HTTP methods (GET, POST, etc.)
          .AllowAnyHeader()); // Allows any headers in requests

// Retrieve the memory cache service
var cache = app.Services.GetRequiredService<IMemoryCache>();

// Define an API endpoint that returns a list of products, including nested 'Category' objects
app.MapGet("/api/productlist", () =>
{
    // Use caching to reduce redundant API calls
    return cache.GetOrCreate("ProductList", entry =>
    {
        // Set cache expiration to 5 minutes
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

        // Predefined product list with category information (generated with AI assistance)
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

// Start the web application and listen for incoming requests
app.Run();
