using System.Net.Http.Json;
using System.Text.Json;
using InventoryHub.Frontend.Pages;

namespace InventoryHub.Frontend.Services
{
    // Servicio para cargar y almacenar los productos de la API
    public class ProductService
    {
        private readonly HttpClient _http;
        public FetchProducts.Product[]? Products { get; private set; }

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        // Método para cargar productos desde el endpoint /api/productlist
        public async Task LoadProductsAsync()
        {
            if (Products == null)
            {
                try
                {
                    // Petición a la API utilizando GetAsync y deserialización con opciones insensibles a mayúsculas
                    var response = await _http.GetAsync("/api/productlist");
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    Products = JsonSerializer.Deserialize<FetchProducts.Product[]>(
                        json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading products: {ex.Message}");
                }
            }
        }
    }
}
