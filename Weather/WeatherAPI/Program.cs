// See https://aka.ms/new-console-template for more information
using System.Net.Cache;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WeatherAPI
{
    class Program
    { 
        static async Task Main(string[] args)
        {                       
            Program program = new Program();
            await program.GetTodoItems();
        }

        private async Task GetTodoItems()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://api.weather.gov/points/29.9007241,-95.5888448");
                
                ProductInfoHeaderValue header = new ProductInfoHeaderValue("WeatherCrow", "1.0");
                ProductInfoHeaderValue comment = new ProductInfoHeaderValue("(+http://www.crowweather.com/WeatherCrow.html)");

                client.DefaultRequestHeaders.UserAgent.Add(header);
                client.DefaultRequestHeaders.UserAgent.Add(comment);

                HttpResponseMessage httpResponseMessage = await client.SendAsync(request);
                string response = await httpResponseMessage.Content.ReadAsStringAsync();

                Console.WriteLine(response);
                    
            }
            
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

