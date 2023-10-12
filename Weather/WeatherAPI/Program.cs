// See https://aka.ms/new-console-template for more information
using System.Net.Cache;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GoogleMaps.LocationServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherAPI.Forecast;
using WeatherAPI.ForecastHourly;

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
                string address = "El Paso, Texas";
                
                // use geocode api to find lat and long by address
                HttpClient locationClient = new HttpClient();
                HttpRequestMessage locationRequest = new HttpRequestMessage(HttpMethod.Get, $"https://geocode.maps.co/search?q={address}");

                HttpResponseMessage locationHttpResponseMessage = await locationClient.SendAsync(locationRequest);
                string locationResponse = await locationHttpResponseMessage.Content.ReadAsStringAsync();
                
                // process locationResponse into usable form for NewtonSoft
                string firstResponse = locationResponse.Split('{', '}')[1];
                firstResponse = "{" + firstResponse + "}";
                
                JObject jsonLocation = JObject.Parse(firstResponse);                               
                JToken? latitude = jsonLocation.SelectToken("lat");
                JToken? longitude = jsonLocation.SelectToken("lon");                
                
                string weatherLocation = $"https://api.weather.gov/points/{latitude},{longitude}";

                // get grid id, x and y for forecast                               
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, weatherLocation);
                
                ProductInfoHeaderValue header = new ProductInfoHeaderValue("WeatherCrow", "1.0");
                ProductInfoHeaderValue comment = new ProductInfoHeaderValue("(+http://www.crowweather.com/WeatherCrow.html)");

                client.DefaultRequestHeaders.UserAgent.Add(header);
                client.DefaultRequestHeaders.UserAgent.Add(comment);

                HttpResponseMessage httpResponseMessage = await client.SendAsync(request);
                string response = await httpResponseMessage.Content.ReadAsStringAsync();

                JObject root = JObject.Parse(response);

                JToken? token = root.SelectToken("properties");
                var xCord = token.SelectToken("gridX");
                var  yCord = token.SelectToken("gridY");
                var  gridID = token.SelectToken("gridId"); 

                HttpRequestMessage forecastRequest = new HttpRequestMessage(HttpMethod.Get, $"https://api.weather.gov/gridpoints/{gridID}/{xCord},{yCord}/forecast");
                HttpResponseMessage httpResponseForecast = await client.SendAsync(forecastRequest); 

                string forcastResponse = await httpResponseForecast.Content.ReadAsStringAsync();

                JObject forecast = (JObject)JObject.Parse(forcastResponse)["properties"];

                JArray dailyForecast = (JArray)forecast["periods"];

                Console.WriteLine(address);
                Console.WriteLine("Temp \t WindSpeed  \t Forecast");
                
                foreach (var day in dailyForecast)
                {
                    var dailyTemp = day["temperature"];
                    var dailyName = day["name"];
                    var dailyWindSpeed = day["windSpeed"];
                    var dailyWindDirection = day["windDirection"];
                    var dailyHumidity = day["relativeHumidity"];
                    var detailedForecast = day["detailedForecast"];

                    Console.WriteLine($"{dailyTemp} \t {dailyWindSpeed} \t {dailyWindDirection} \t {dailyName} : {detailedForecast}");
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

