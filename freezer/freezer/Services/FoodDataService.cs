using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace FreezerWebPages.Services
{
    public class FoodDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string apiKey = "yodlw1wia4uws1442neooa76bgtxi3"; // Use your actual API key
        private readonly string baseUrl = "https://api.barcodelookup.com/v3/products";

        public FoodDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(string Name, string Category)> GetFoodItemNameByUPCAsync(string upcCode)
        {
            var requestUrl = $"{baseUrl}?barcode={upcCode}&formatted=y&key={apiKey}";

            Console.WriteLine($"Making API call to: {requestUrl}");

            var response = await _httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Response: {content}");

                if (!string.IsNullOrEmpty(content))
                {
                    var json = JObject.Parse(content);

                    if (json["products"] is JArray productsArray && productsArray.Count > 0)
                    {
                        var product = productsArray[0];
                        var productName = product["title"]?.ToString();
                        var category = product["category"].ToString().Split(new[] { '>' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault().Trim();

                        Console.WriteLine($"Product Found: {productName}, Category: {category}");
                        return (productName, category);
                    }
                    else
                    {
                        Console.WriteLine("No products found in the response.");
                    }
                }
                else
                {
                    Console.WriteLine("Response content is empty or null.");
                }
            }
            else
            {
                Console.WriteLine($"API call failed with status code: {response.StatusCode}");
                return (null, null); // Indicate that no product was found
            }

         return (null, null);   
        }
    }
}