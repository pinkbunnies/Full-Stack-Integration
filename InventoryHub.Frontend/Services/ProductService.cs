using System.Net.Http.Json; // Provides extension methods for JSON operations over HTTP
using System.Text.Json; // Provides functionality for JSON serialization and deserialization
using InventoryHub.Frontend.Pages; // Imports the pages from the InventoryHub Frontend

namespace InventoryHub.Frontend.Services
{
    // Service to load and store products from the API
    public class ProductService
    {
        // Private HttpClient instance used for making HTTP requests
        private readonly HttpClient _http;

        // Public property to hold an array of products fetched from the API; nullable if not loaded yet
        public FetchProducts.Product[]? Products { get; private set; }

        // Constructor that initializes the ProductService with a provided HttpClient
        public ProductService(HttpClient http)
        {
            _http = http;
        }

        // Asynchronous method to load products from the "/api/productlist" endpoint
        public async Task LoadProductsAsync()
        {
            // Check if products have not been loaded yet to avoid redundant calls
            if (Products == null)
            {
                try
                {
                    // Make a GET request to the API endpoint "/api/productlist"
                    var response = await _http.GetAsync("/api/productlist");
                    
                    // Ensure the response indicates success; throw an exception otherwise
                    response.EnsureSuccessStatusCode();
                    
                    // Read the response content as a JSON string
                    var json = await response.Content.ReadAsStringAsync();
                    
                    // Deserialize the JSON string into an array of Product objects.
                    // The deserialization is configured to be case-insensitive with respect to property names.
                    Products = JsonSerializer.Deserialize<FetchProducts.Product[]>(
                        json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );
                }
                catch (Exception ex)
                {
                    // Log any errors that occur during the HTTP request or deserialization process
                    Console.WriteLine($"Error loading products: {ex.Message}");
                }
            }
        }
    }
}
